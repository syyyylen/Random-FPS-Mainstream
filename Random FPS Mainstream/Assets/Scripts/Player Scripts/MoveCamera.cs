using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform m_camPosition;

    private void Update()
    {
        transform.position = m_camPosition.position;
    }
}
