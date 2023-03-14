using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_animator;

    private Vector3 m_velocity;

    private bool m_wasGrounded;
    private bool m_isGrounded = true;

    private float stamina = 100.0f;
    private int running = 0;

    public float m_moveSpeed = 2.0f;
    public float m_jumpForce = 5.0f;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);
        PlayerMove();
        JumpingAndLanding();

        if (stamina < 100.0f && running == 0)
        {
            stamina += 20.0f * Time.deltaTime;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void PlayerMove()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float gravity = 10.0f; // rigidbody가 없기 때문에 중력을 별도로 계산

        if (controller.isGrounded)
        {
            m_velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            m_velocity = m_velocity.normalized;

            if (Input.GetKey(KeyCode.LeftShift))
            {               
                if (stamina > 0.0f)
                {
                    m_velocity *= 2.0f;
                    stamina -= 40.0f * Time.deltaTime;
                    running = 1;
                }
            }
            else { running = 0; }
            m_animator.SetFloat("MoveSpeed", m_velocity.magnitude);

            if (Input.GetButtonDown("Jump"))
            {
                if (gameObject.CompareTag("Cat"))
                {
                    m_velocity.y = m_jumpForce;
                }
                else if (gameObject.CompareTag("Dog"))
                {
                    m_velocity *= 2.0f;
                    m_velocity.y = m_jumpForce / 1.3f;
                }
                else { }
            }
            else if (m_velocity.magnitude > 0.5)
            {
                transform.LookAt(transform.position + m_velocity);
            }
        }
        m_velocity.y -= gravity * Time.deltaTime;
        controller.Move(m_velocity * m_moveSpeed * Time.deltaTime);
        m_isGrounded = controller.isGrounded;
    }

    private void JumpingAndLanding()
    {
        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
}
