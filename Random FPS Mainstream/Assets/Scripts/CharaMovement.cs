using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class CharaMovement : MonoBehaviour
{
    private float m_playerHeight = 2.0f;

    [SerializeField] private Transform m_orientation;

    [Header("Bindings")] 
    [SerializeField] private KeyCode m_jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode m_sprintKey = KeyCode.LeftShift;
    
    [Header("Movement")] 
    public float m_moveSpeed = 6.0f; 
    private float m_movMultiplier = 10.0f;
    [SerializeField] private float m_airMultiplier = 0.4f;
    private float m_horizontalMov;
    private float m_verticalMov;
    private Vector3 m_moveDirection;

    [Header("Sprinting")] 
    [SerializeField] private float m_walkSpeed = 4.0f;
    [SerializeField] private float m_sprintSpeed = 6.0f;
    [SerializeField] private float m_accelerationSpeed = 10.0f;
    
    [Header("Drag")]
    private float m_groundedDrag = 6.0f;
    private float m_airDrag = 2.0f;

    [Header("Jumping")] 
    [SerializeField] private float m_jumpForce = 5.0f;

    [Header("Ground Detection")] [SerializeField]
    private Transform m_groundCheck;
    private bool m_isGrounded;
    private float m_groundDistance = 0.4f;
    [SerializeField] private LayerMask m_groundMask;
    
    //Rigidbody
    private Rigidbody m_playerRb;
    
    //About Slopes
    private RaycastHit m_slopeHit;
    private Vector3 m_slopeMoveDirection;

    private bool m_onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out m_slopeHit, m_playerHeight / 2 + 0.5f)) ;
        {
            if(m_slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody>();
        m_playerRb.freezeRotation = true;
    }

    private void Update()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);
        
        PlayerInputs();
        ControlSpeed();
        ControlDrag();

        if (Input.GetKeyDown(m_jumpKey) && m_isGrounded)
        {
            PlayerJump();
        }

        m_slopeMoveDirection = Vector3.ProjectOnPlane(m_moveDirection, m_slopeHit.normal);
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
        if (m_isGrounded && !m_onSlope())
        {
            m_playerRb.AddForce(m_moveDirection.normalized * m_moveSpeed * m_movMultiplier, ForceMode.Acceleration);
        }
        else if (m_isGrounded && m_onSlope())
        {
            m_playerRb.AddForce(m_slopeMoveDirection.normalized * m_moveSpeed * m_movMultiplier, ForceMode.Acceleration);
        }
        else if (!m_isGrounded)
        {
            m_playerRb.AddForce(m_moveDirection.normalized * m_moveSpeed * m_movMultiplier * m_airMultiplier, ForceMode.Acceleration);

        }
    }

    private void PlayerJump()
    {
        m_playerRb.velocity = new Vector3(m_playerRb.velocity.x, 0, m_playerRb.velocity.z);
        m_playerRb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
    }

    private void ControlSpeed()
    {
        if (Input.GetKey(m_sprintKey) && m_isGrounded)
        {
            m_moveSpeed = Mathf.Lerp(m_moveSpeed, m_sprintSpeed, m_accelerationSpeed * Time.deltaTime);
        }
        else
        {
            m_moveSpeed = Mathf.Lerp(m_moveSpeed, m_walkSpeed, m_accelerationSpeed * Time.deltaTime);
        }
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
