using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "GD2/Ammo")]

public class WeaponSO : ScriptableObject
{
    [Header("Weapon Name")]
    public string m_weaponName;
    
    [Header("Weapon Prefab Modé 3D")]
    public GameObject m_weaponPrefab;
    
    [Header("Weapons Stats")]
    public int m_weaponDamages = 0;
    
    public int m_weaponMagazine = 0;
    
    public float m_weaponRateOfFire = 0.0f;
    
    public float m_weaponReloadTime = 0.0f;
    
    public float m_weaponRecoil = 0.0f;

    public override string ToString()
    {
        return $"Weapon Name : {m_weaponName}, dealing {m_weaponDamages} with a {m_weaponRateOfFire} Rate Of Fire. Contains {m_weaponMagazine} bullets in magazine and reloads in {m_weaponReloadTime} seconds.";
    }
}
