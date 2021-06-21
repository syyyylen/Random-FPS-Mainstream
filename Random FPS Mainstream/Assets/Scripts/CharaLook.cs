using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaLook : MonoBehaviour
{
    [SerializeField] private float m_sensX = 100.0f;
    [SerializeField] private float m_sensY = 100.0f;

    private Camera m_playerCamera;

    private float m_mouseX;
    private float m_mouseY;

    private float m_multiplier = 0.01f;

    private float m_xRotation;
    private float m_yRotation;

    private void Start()
    {
        m_playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerInput();

        m_playerCamera.transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, m_yRotation, 0);
    }

    private void PlayerInput()
    {
        m_mouseX = Input.GetAxisRaw("Mouse X");
        m_mouseY = Input.GetAxisRaw("Mouse Y");

        m_yRotation += m_mouseX * m_sensX * m_multiplier;
        m_xRotation -= m_mouseY * m_sensY * m_multiplier;

        m_xRotation = Mathf.Clamp(m_xRotation, -90.0f, 90.0f);
    }
}
