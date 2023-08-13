using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string GunName;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public float spread;
    public bool isAutomatic;
    public int bulletsPerShot;

    [Header("Reloading")]
    public int magazineSize;
    public int fireRate;

    [HideInInspector]
    public bool isReloading;
}
