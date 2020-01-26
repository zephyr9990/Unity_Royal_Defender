using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo
{
    public string weaponName;
    public int damage;
    public int maxDurability;
    public GameObject weaponMesh;
    private int currentDurability;
    public WeaponType type;

    public WeaponInfo(WeaponProperties weaponProperties)
    {
        weaponName = weaponProperties.weaponName;
        damage = weaponProperties.damage;
        maxDurability = weaponProperties.maxDurability;
        weaponMesh = weaponProperties.weaponMesh;
        currentDurability = maxDurability;
        type = weaponProperties.type;
    }
}
