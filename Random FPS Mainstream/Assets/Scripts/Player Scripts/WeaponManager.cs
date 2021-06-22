using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Currently equiped player's weapon")]
    [SerializeField] private WeaponSO m_weapon;

    [Header("Weapon Prefab taken from WeaponSO")]
    private GameObject m_weaponPrefab;

    [SerializeField] private Transform m_orientation;
    private void Awake()
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
    }

    private void MoveWeapon()
    {
        m_weaponPrefab.transform.position = transform.position;
        m_weaponPrefab.transform.rotation = m_orientation.transform.rotation;
    }
}
