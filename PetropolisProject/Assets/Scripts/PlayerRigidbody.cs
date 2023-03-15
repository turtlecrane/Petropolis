using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbody : MonoBehaviour
{
    private Animator m_animator;

    private Rigidbody m_rigidBody;
    private bool m_wasGrounded; // 이전에 땅위에 있었으면 true 공중에 있었으면 false
    private bool m_isGrounded; // 현재 땅위에 있으면 true 공중에 있으면 false
    private List<Collider> m_collisions = new List<Collider>();

    private float stamina = 100.0f; // 달리기 스태미나
    private float recovery_stamina = 20.0f; // 대기, 걷기 중 스태미나 회복량
    private float reduction_stamina = 40.0f; // 대기, 걷기 중 스태미나 감소량
    private int running = 0; // 0 = 대기, 걷기 중 / 1 = 달리기 중

    public float cat_jumpvalue = 1.5f; // 고양이 점프 시 높이, 전진 가중치
    public float cat_forwardvalue = 0.0f;
    public float dog_jumpvalue = 1.2f; // 개 점프 시 높이, 전진 가중치
    public float dog_forwardvalue = 3.0f;

    public float m_moveSpeed = 2.0f; // 이동속도
    public float m_jumpForce = 3.0f; // 점프의 힘
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
            if (stamina > 0.0f) // 스태미나가 남아 있을 때
            {
                velocity *= 2.0f; // MoveSpeed에 값을 전달할때 보다 효율적이기 위해 velocity를 전달
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
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval; // 쿨타임

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

        if (!m_wasGrounded && m_isGrounded) // 공중에 떠있었고 지금 땅에 착지
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded) // 땅에 서있었고 공중으로 점프
        {
            m_animator.SetTrigger("Jump");
        }

    }

    private void OnCollisionEnter(Collision collision) // 충돌한 순간
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f) // 내적을 계산하여 땅 위에 있는지 없는지를 판단
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }


    private void OnCollisionExit(Collision collision) // 충돌체와 떨어짐
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    private void OnCollisionStay(Collision collision) // 충돌체와 계속 붙어있음
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
