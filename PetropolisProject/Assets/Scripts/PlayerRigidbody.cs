using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbody : MonoBehaviour
{
    private Animator m_animator;

    private Rigidbody m_rigidBody;
    private bool m_wasGrounded; // ������ ������ �־����� true ���߿� �־����� false
    private bool m_isGrounded; // ���� ������ ������ true ���߿� ������ false
    private List<Collider> m_collisions = new List<Collider>();

    private float stamina = 100.0f; // �޸��� ���¹̳�
    private float recovery_stamina = 20.0f; // ���, �ȱ� �� ���¹̳� ȸ����
    private float reduction_stamina = 40.0f; // ���, �ȱ� �� ���¹̳� ���ҷ�
    private int running = 0; // 0 = ���, �ȱ� �� / 1 = �޸��� ��

    public float cat_jumpvalue = 1.5f; // ����� ���� �� ����, ���� ����ġ
    public float cat_forwardvalue = 0.0f;
    public float dog_jumpvalue = 1.2f; // �� ���� �� ����, ���� ����ġ
    public float dog_forwardvalue = 3.0f;

    public float m_moveSpeed = 2.0f; // �̵��ӵ�
    public float m_jumpForce = 3.0f; // ������ ��
    private float m_jumpTimeStamp = 0; 
    private float m_minJumpInterval = 0.25f;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        PlayerMove();
        JumpingAndLanding();

        if (stamina < 100.0f && running == 0)
        {
            stamina += recovery_stamina * Time.deltaTime;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = Vector3.right * h;
        Vector3 moveVertical = Vector3.forward * v;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized;

        transform.LookAt(transform.position + velocity);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
        {
            if (stamina > 0.0f) // ���¹̳��� ���� ���� ��
            {
                velocity *= 2.0f; // MoveSpeed�� ���� �����Ҷ� ���� ȿ�����̱� ���� velocity�� ����
                stamina -= reduction_stamina * Time.deltaTime;
                running = 1;
            }    
        }
        else { running = 0; }

        transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);

        m_animator.SetFloat("MoveSpeed", velocity.magnitude);
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval; // ��Ÿ��

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;

            if (gameObject.CompareTag("Cat"))
            {
                m_rigidBody.AddRelativeForce(Vector3.forward * cat_forwardvalue, ForceMode.Impulse);
                m_rigidBody.AddForce(Vector3.up * m_jumpForce * cat_jumpvalue, ForceMode.Impulse);
            }
            else if (gameObject.CompareTag("Dog"))
            {
                m_rigidBody.AddRelativeForce(Vector3.forward * dog_forwardvalue, ForceMode.Impulse);
                m_rigidBody.AddForce(Vector3.up * m_jumpForce * dog_jumpvalue, ForceMode.Impulse);
            }
        }

        if (!m_wasGrounded && m_isGrounded) // ���߿� ���־��� ���� ���� ����
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded) // ���� ���־��� �������� ����
        {
            m_animator.SetTrigger("Jump");
        }

    }

    private void OnCollisionEnter(Collision collision) // �浹�� ����
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f) // ������ ����Ͽ� �� ���� �ִ��� �������� �Ǵ�
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }


    private void OnCollisionExit(Collision collision) // �浹ü�� ������
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    private void OnCollisionStay(Collision collision) // �浹ü�� ��� �پ�����
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }
}
