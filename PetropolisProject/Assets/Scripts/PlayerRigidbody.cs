using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerRigidbody : MonoBehaviour
{
    private Animator m_animator;
    private Camera playercam;  // 플레이어를 추적하는 카메라

    private Rigidbody m_rigidBody;
    private bool m_wasGrounded; // 이전에 땅위에 있었으면 true 공중에 있었으면 false
    private bool m_isGrounded; // 현재 땅위에 있으면 true 공중에 있으면 false
    private List<Collider> m_collisions = new List<Collider>();

    private bool isDeath = false;

    public float stamina = 100.0f; // 달리기 스태미나
    public float recovery_stamina = 20.0f; // 대기, 걷기 중 스태미나 회복량
    public float reduction_stamina = 40.0f; // 대기, 걷기 중 스태미나 감소량
    public int running = 0; // 0 = 대기, 걷기 중 / 1 = 달리기 중

    private float forwardvalue = 0.0f;
    private float jumpvalue = 0.0f;

    public float cat_jumpvalue = 1.5f; // 고양이 점프 시 높이, 전진 가중치
    public float cat_forwardvalue = 0.0f;
    public float dog_jumpvalue = 1.3f; // 개 점프 시 높이, 전진 가중치
    public float dog_forwardvalue = 3.0f;

    public float m_moveSpeed = 8.0f; // 이동속도
    public float m_jumpForce = 3.0f; // 점프의 힘
    private float m_jumpTimeStamp = 0; 
    private float m_minJumpInterval = 0.25f;

    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        playercam = Camera.main;
    }

    void FixedUpdate()
    {
        if (!isDeath)
        {
            m_animator.SetBool("Grounded", m_isGrounded);

            PlayerMove();
            JumpingAndLanding();
            PlaySound();

            if (stamina < 100.0f && running == 0) // 스태미나가 100보다 적고, 달리고 있지 않을 때
            {
                stamina += recovery_stamina * Time.deltaTime;
            }
            m_wasGrounded = m_isGrounded;
            //
        }
    }
    

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = new Vector3(playercam.transform.right.x, 0.0f, playercam.transform.right.z) * h;
        Vector3 moveVertical = new Vector3(playercam.transform.forward.x, 0.0f, playercam.transform.forward.z) * v;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized;

        //transform.LookAt(transform.position + velocity);
        if (velocity != Vector3.zero)
        {
            transform.forward = velocity;
        }

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
        
        m_rigidBody.velocity = new Vector3(velocity.x * m_moveSpeed, m_rigidBody.velocity.y, velocity.z * m_moveSpeed);
        //transform.Translate(velocity * m_moveSpeed * Time.deltaTime, Space.World);
        //m_rigidBody.MovePosition(transform.position + velocity * m_moveSpeed * Time.deltaTime);
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
                forwardvalue = cat_forwardvalue;
                jumpvalue = cat_jumpvalue;
            }
            else if (gameObject.CompareTag("Dog"))
            {
                forwardvalue = dog_forwardvalue;
                jumpvalue = dog_jumpvalue;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_rigidBody.AddRelativeForce(Vector3.forward * forwardvalue, ForceMode.Impulse);
            }
            m_rigidBody.AddForce(Vector3.up * m_jumpForce * jumpvalue, ForceMode.Impulse);
            //m_rigidBody.velocity = Vector3.up * m_jumpForce * jumpvalue;
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

        if (collision.collider.CompareTag("Chair"))
        {
            float cnt = 0;
            if (Input.GetMouseButtonDown(0))
            {
                cnt++;
                var obj = collision.gameObject;
                /*Vector3 targetpos = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
                transform.position = targetpos;*/
                transform.position = obj.transform.position; //충돌한 오브젝트의 위치로 이동(=의자로 이동)
                m_moveSpeed = 0; // 움직임 멈추게 하기
                PlayerSit();
            }
            /*if (m_animator.GetBool("Sit") == true && cnt <= 2.0f)
            {
                PlayerStandup();
            }*/
        }

    }

    public void PlaySound()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            audioSource.PlayOneShot(audioClip);
    }

    public float sitcnt = 4.0f;
    private void PlayerSit()
    {
        m_animator.SetBool("Sit", true);  
        Invoke("PlayerStandup", sitcnt);  // 4초 뒤 다시 일어남
    }
    private void PlayerStandup()
    {
        m_animator.SetBool("StandUp", true);
        m_animator.SetBool("Sit", false);
        Invoke("move", 0.5f); // 다시 움직임
    }
    private void move()
    {
        m_moveSpeed = 8.0f;
    }

    public void PlayerDeath()
    {
        isDeath = true;
        m_animator.SetTrigger("isDeath");
        transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("Directional Light").gameObject.SetActive(false);
    }
}
