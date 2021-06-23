using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Currently equiped player's weapon")]
    [SerializeField] private WeaponSO m_weapon;

    [Header("Weapon Prefab taken from WeaponSO")]
    private GameObject m_weaponPrefab;

    [SerializeField] private Camera m_playerCamera;
    [SerializeField] private Transform m_orientation;
    
    private float m_range = 500.0f;

    private void Start()
    {
        SetEquipedWeapon(m_weapon);
    }

    private void SetEquipedWeapon(WeaponSO p_newWeapon)
    {
        m_weapon = p_newWeapon;
        m_weaponPrefab = Instantiate(m_weapon.m_weaponPrefab, transform.position, transform.rotation);
    }

    private void Update()
    {
        MoveWeapon();

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    private void MoveWeapon()
    {
        m_weaponPrefab.transform.position = transform.position;
        m_weaponPrefab.transform.rotation = m_orientation.transform.rotation;
    }

    private void Fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_playerCamera.transform.position, m_playerCamera.transform.forward, out hit, m_range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
