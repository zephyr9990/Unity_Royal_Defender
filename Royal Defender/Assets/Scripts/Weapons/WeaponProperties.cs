using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public int maxDurability;
    public GameObject weaponMesh;
    public Texture2D weaponIcon;
    public WeaponType type;
    public float attackDelay;
    public float npcAttackDelay;
}
