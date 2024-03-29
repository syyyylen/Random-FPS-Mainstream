﻿using System.Collections;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Camera m_playerCamera;
    
    [Header("Currently equiped player's weapon")]
    [SerializeField] private WeaponSO m_weapon;

    [Header("HitMarker")]
    [SerializeField] private GameObject m_hitMarker;
    private Coroutine m_hitMarkerRoutine;
    private float m_hitMarkerDuration = 0.1f;
    
    private float m_range = 100.0f;
    private float m_fireTimer;
    private int m_bulletsInMag;
    [SerializeField] private TextMeshProUGUI m_bulletsInMagText;
    private LayerMask m_layerEnnemis;

    //About Reload 
    private KeyCode m_reloadKey = KeyCode.R;
    private bool m_isReloading = false;
    private Coroutine m_reloadRoutine;
    [SerializeField] private GameObject m_reloadText;
    
    private void Start()
    {
        m_layerEnnemis = LayerMask.GetMask("Ennemis");
        SetEquipedWeapon(m_weapon);
        m_bulletsInMag = m_weapon.m_weaponMagazine;
        UpdateUI();
    }

    private void SetEquipedWeapon(WeaponSO p_newWeapon)
    {
        m_weapon = p_newWeapon;
    }

    private void Update()
    {
        //MoveWeapon();

        if (Input.GetKeyDown(m_reloadKey))
        {
            Reload();
        }

        if (m_isReloading != true &  m_bulletsInMag > 0 & Input.GetButton("Fire1"))
        {
            Fire();
        }

        if (m_fireTimer < m_weapon.m_weaponRateOfFire)
            m_fireTimer += Time.deltaTime;
    }

    private void Fire()
    {
        if (m_fireTimer < m_weapon.m_weaponRateOfFire) return;

        m_bulletsInMag -= 1;
        UpdateUI();
        
        RaycastHit hit;
        if (Physics.Raycast(m_playerCamera.transform.position, m_playerCamera.transform.forward, out hit, m_range, m_layerEnnemis))
        {
            Debug.Log(hit.transform.name);
            HitMarker();
        }

        m_fireTimer = 0.0f;
    }

    private void HitMarker()
    {
        if(m_hitMarkerRoutine != null)
            StopCoroutine(m_hitMarkerRoutine);
        m_hitMarkerRoutine = StartCoroutine(HitMarkerRoutine());
        m_hitMarker.SetActive(true);
    }

    private IEnumerator HitMarkerRoutine()
    {
        yield return new WaitForSeconds(m_hitMarkerDuration);
        m_hitMarker.SetActive(false);
        
    }

    private void Reload()
    {
        m_isReloading = true;
        m_reloadText.SetActive(true);
        if(m_reloadRoutine != null)
            StopCoroutine(m_reloadRoutine);
        m_reloadRoutine = StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(m_weapon.m_weaponReloadTime);
        m_bulletsInMag = m_weapon.m_weaponMagazine;
        UpdateUI();
        m_isReloading = false;
        m_reloadText.SetActive(false);
    }

    private void UpdateUI()
    {
        m_bulletsInMagText.text = m_bulletsInMag + "/" + m_weapon.m_weaponMagazine;
    }
}
