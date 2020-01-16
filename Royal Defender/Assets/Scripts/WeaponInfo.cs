using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public int maxDurability;
    public GameObject weaponMesh;
    private int currentDurability;

    WeaponInfo()
    {
        currentDurability = maxDurability;
    }

    public int GetCurrentDurability()
    {
        return currentDurability;
    }
}
