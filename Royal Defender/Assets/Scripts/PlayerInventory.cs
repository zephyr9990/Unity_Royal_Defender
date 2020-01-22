using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // components
    private Animator animator;
    private EquippedWeapon equippedWeapon;

    // weapons
    private WeaponInfo currentRangedWeapon;
    private int currentRangedWeaponIndex;
    private WeaponInfo currentMeleeWeapon;
    private int currentMeleeWeaponIndex;

    public int maxAmmo;
    public int startingAmmo;
    private int currentAmmo;

    // inventory
    public int maxNumberOfRangedWeapons;
    private ArrayList rangedWeapons;
    
    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = GetComponent<EquippedWeapon>();

        // weapons
        currentRangedWeapon = null;
        currentRangedWeaponIndex = -1;
        currentMeleeWeapon = null;
        currentMeleeWeaponIndex = -1;

        currentAmmo = startingAmmo;

        rangedWeapons = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SwitchRanged"))
        {
            // Passes the up or down direction the player has pressed
            // to change the weapon index value
            int direction = (int) Input.GetAxis("SwitchRanged");
            SwitchRanged(direction);
        }
    }

    public void AddRangedWeapon(WeaponInfo rangedWeapon)
    {
        // limit number of ranged weapons to 5
        if (rangedWeapons.Count < maxNumberOfRangedWeapons)
        {
            rangedWeapons.Add(rangedWeapon);
        }
        Debug.LogWarning(rangedWeapons.Count);

    }

    public int getCurrentAmmo() 
    {
        return currentAmmo;
    }

    private void SwitchRanged(int direction)
    {
        int desiredWeaponIndex = currentRangedWeaponIndex + direction;
        if (desiredWeaponIndex >= rangedWeapons.Count)
        {
            // Don't play effect if there are no more weapons.
            Debug.LogWarning("Switching to ranged weapon index: " + currentRangedWeaponIndex);
            return;
        }
        else if (desiredWeaponIndex >=0 && desiredWeaponIndex <= rangedWeapons.Count - 1)
        {
            currentRangedWeapon = (WeaponInfo) rangedWeapons[desiredWeaponIndex];
            currentRangedWeaponIndex = desiredWeaponIndex;
        }
        else if (desiredWeaponIndex <= -1) // Unarmed
        {
            currentRangedWeapon = null;
            currentRangedWeaponIndex = -1;
        }
        
        EquipWeapon(currentRangedWeapon);
        Debug.LogWarning("Switching to ranged weapon index: " + currentRangedWeaponIndex);
    }

    private void EquipWeapon(WeaponInfo weapon)
    {
        equippedWeapon.EquipWeapon(weapon);
    }

    private void UnequipWeapon(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Ranged)
            currentRangedWeaponIndex = -1;
        else if (weaponType == WeaponType.Melee)
            currentMeleeWeaponIndex = -1;
    }
}
