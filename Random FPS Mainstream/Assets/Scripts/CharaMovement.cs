using System;
using UnityEngine;

public class CharaMovement : MonoBehaviour
{
    private float m_playerHeight = 2.0f;

    [SerializeField] private Transform m_orientation;

    [Header("Bindings")] [SerializeField] private KeyCode m_jumpKey = KeyCode.Space;
    
    [Header("Movement")] 
    public float m_moveSpeed = 6.0f; 
    private float m_movMultiplier = 10.0f;
    [SerializeField] private float m_airMultiplier = 0.4f;
    private float m_horizontalMov;
    private float m_verticalMov;
    private Vector3 m_moveDirection;
    
    [Header("Drag")]
    private float m_groundedDrag = 6.0f;
    private float m_airDrag = 2.0f;

    [Header("Jumping")] 
    [SerializeField] private float m_jumpForce = 5.0f;
    
    [Header("Ground Detection")]
    private bool m_isGrounded;
    private float m_groundDistance = 0.4f;
    [SerializeField] private LayerMask m_groundMask;
    
    //Rigidbody
    private Rigidbody m_playerRb;

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody>();
        m_playerRb.freezeRotation = true;
    }

    private void Update()
    {
        m_isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), m_groundDistance, m_groundMask);
        
        PlayerInputs();
        ControlDrag();

        if (Input.GetKeyDown(m_jumpKey) && m_isGrounded)
        {
            PlayerJump();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInputs()
    {
        m_horizontalMov = Input.GetAxisRaw("Horizontal");
        m_verticalMov = Input.GetAxisRaw("Vertical");

        m_moveDirection = m_orientation.forward * m_verticalMov + m_orientation.right * m_horizontalMov;
    }

    private void PlayerMove()
    {
        if (m_isGrounded)
        {
            m_playerRb.AddForce(m_moveDirection.normalized * m_moveSpeed * m_movMultiplier, ForceMode.Acceleration);
        }
        else if (!m_isGrounded)
        {
            m_playerRb.AddForce(m_moveDirection.normalized * m_moveSpeed * m_movMultiplier * m_airMultiplier, ForceMode.Acceleration);

        }
    }

    private void PlayerJump()
    {
        m_playerRb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
    }

    private void ControlDrag()
    {
        if (m_isGrounded)
        {
            m_playerRb.drag = m_groundedDrag;
        }
        else
        {
            m_playerRb.drag = m_airDrag;
        }
    }
}
