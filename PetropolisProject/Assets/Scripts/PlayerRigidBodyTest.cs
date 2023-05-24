using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidBodyTest : MonoBehaviour
{
    private Animator m_animator;

    private Rigidbody m_rigidBody;
    private bool m_wasGrounded; // 이전에 땅위에 있었으면 true 공중에 있었으면 false
    private bool m_isGrounded; // 현재 땅위에 있으면 true 공중에 있으면 false
    private List<Collider> m_collisions = new List<Collider>();

    public float m_moveSpeed = 2.0f; // 이동속도
    public float m_jumpForce = 5.0f; // 점프의 힘
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity *= 2.0f; // MoveSpeed에 값을 전달할때 보다 효율적이기 위해 velocity를 전달
        }
        transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
        //m_rigidBody.velocity = velocity * m_moveSpeed;
        //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);

        m_animator.SetFloat("MoveSpeed", velocity.magnitude);
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval; // 쿨타임

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
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
