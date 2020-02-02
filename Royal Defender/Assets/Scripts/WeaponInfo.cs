using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo
{
    public string name;
    public int damage;
    public int maxDurability;
    public GameObject mesh;
    public Texture2D icon;
    private int currentDurability;
    public WeaponType type;

    public WeaponInfo(WeaponProperties weaponProperties)
    {
        name = weaponProperties.weaponName;
        damage = weaponProperties.damage;
        maxDurability = weaponProperties.maxDurability;
        mesh = weaponProperties.weaponMesh;
        currentDurability = maxDurability;
        icon = weaponProperties.weaponIcon;
        type = weaponProperties.type;
    }

    public float getDurabilityPercent()
    {
        return (float)currentDurability / maxDurability;
    }

    public int getCurrentDurability()
    {
        return currentDurability;
    }

    public void DecreaseDurability(int amount)
    {
        currentDurability -= amount;
    }
}
