using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo
{
    public string weaponName;
    public int damage;
    public int maxDurability;
    public GameObject weaponMesh;
    public Texture2D icon;
    private int currentDurability;
    public WeaponType type;

    public WeaponInfo(WeaponProperties weaponProperties)
    {
        weaponName = weaponProperties.weaponName;
        damage = weaponProperties.damage;
        maxDurability = weaponProperties.maxDurability;
        weaponMesh = weaponProperties.weaponMesh;
        currentDurability = maxDurability;
        icon = weaponProperties.weaponIcon;
        type = weaponProperties.type;
    }

    public float getWeaponDurabilityPercent()
    {
        return currentDurability / maxDurability;
    }
}
