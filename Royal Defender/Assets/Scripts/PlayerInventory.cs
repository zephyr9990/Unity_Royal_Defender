using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // components
    public Animator animator;
    private EquippedWeapon equippedWeapon;
    public Animator weaponPanelAnimator;

    // weapons
    private int currentRangedWeaponIndex;
    private int currentMeleeWeaponIndex;
    private WeaponType currentType;

    public int maxAmmo;
    public int startingAmmo;
    private int currentAmmo;

    private bool listSwitched;

    // inventory
    public int maxNumberOfRangedWeapons;
    public int maxNumberOfMeleeWeapons;
    private ArrayList rangedWeapons;
    private ArrayList meleeWeapons;
    private ArrayList currentWeaponList;
    
    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = GetComponent<EquippedWeapon>();

        // weapons
        currentRangedWeaponIndex = -1;
        currentMeleeWeaponIndex = -1;

        currentAmmo = startingAmmo;

        rangedWeapons = new ArrayList();
        meleeWeapons = new ArrayList();
        listSwitched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SwitchWeaponType"))
        {
            // Player cannot switch weapons if they are swinging.
            bool PlayerIsSwinging = animator.GetBool("IsSwinging");
            if (!PlayerIsSwinging)
            {
                int direction = (int)Input.GetAxis("SwitchWeaponType");
                SwitchWeaponList(direction);

                // Only need to equip a different weapon if list has been switched.
                if (listSwitched)
                {
                    SwitchWeapon(currentWeaponList, 0);
                }
            }
        }

        if (Input.GetButtonDown("SwitchWeapon"))
        {
            // Passes the up or down direction the player has pressed
            // to change the weapon index value
            int direction = (int)Input.GetAxis("SwitchWeapon");
            SwitchWeapon(currentWeaponList, direction);
        }
    }

    public void AddWeapon(WeaponInfo weapon)
    {
        if (weapon.type == WeaponType.Ranged)
        {
            // limit number of ranged weapons according to max number
            if (rangedWeapons.Count < maxNumberOfRangedWeapons)
            {
                rangedWeapons.Add(weapon);
            }
            Debug.LogWarning("Ranged weapon count: " + rangedWeapons.Count);
        }
        else if (weapon.type == WeaponType.Melee)
        {
            // limit number of melee weapons according to max number
            if (meleeWeapons.Count < maxNumberOfMeleeWeapons)
            {
                meleeWeapons.Add(weapon);
            }
            Debug.LogWarning("Melee weapon count: " + meleeWeapons.Count);
        }
    }

    public int getCurrentAmmo() 
    {
        return currentAmmo;
    }

    private void SwitchWeapon(ArrayList weapons, int direction)
    {
        if (weapons == null || currentType == WeaponType.NULL)
        {
            return; // have not selected a list, so backout
        }

        int currentWeaponIndex = GetCurrentWeaponIndex();        
        int desiredWeaponIndex = currentWeaponIndex + direction;
        WeaponInfo weaponToEquip = null;
        if (desiredWeaponIndex >= weapons.Count)
        {
            // Don't play effect if there are no more weapons.
            Debug.LogWarning("Switching to " + currentType.ToString() + " weapon index: " + currentWeaponIndex);
            return;
        }
        else if (desiredWeaponIndex >=0 && desiredWeaponIndex <= weapons.Count - 1)
        {
            weaponToEquip = (WeaponInfo)weapons[desiredWeaponIndex];
            currentWeaponIndex = desiredWeaponIndex;
        }
        else if (desiredWeaponIndex <= -1) // Unarmed
        {
            weaponToEquip = null;
            currentWeaponIndex = -1;
        }

        SetCurrentWeaponIndex(currentWeaponIndex);
        EquipWeapon(weaponToEquip);
        Debug.LogWarning("Switching to " + currentType.ToString() + " weapon index: " + currentWeaponIndex);
    }

    private void SwitchWeaponList(int direction)
    {
        listSwitched = false;
        if (direction == 1) // Up pressed. User wants ranged weapons
        {
            if (currentWeaponList == meleeWeapons || currentWeaponList == null)
            {
                currentType = WeaponType.Ranged;
                currentWeaponList = rangedWeapons;
                weaponPanelAnimator.SetBool("MeleeListIsActive", false);
                weaponPanelAnimator.SetBool("RangedListIsActive", true);

                listSwitched = true;
            }
        }
        else // User wants melee list
        {
            if (currentWeaponList == rangedWeapons || currentWeaponList == null)
            {
                currentType = WeaponType.Melee;
                currentWeaponList = meleeWeapons;
                weaponPanelAnimator.SetBool("MeleeListIsActive", true);
                weaponPanelAnimator.SetBool("RangedListIsActive", false);

                listSwitched = true;
            }
        }
    }

    private int GetCurrentWeaponIndex()
    {
        if (currentType == WeaponType.Ranged)
        {
            return currentRangedWeaponIndex;
        }
        else
        {
            return currentMeleeWeaponIndex;
        }
    }

    private void SetCurrentWeaponIndex(int index)
    {
        if (currentType == WeaponType.Ranged)
        {
            currentRangedWeaponIndex = index;
        }
        else
        {
            currentMeleeWeaponIndex = index;
        }
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
