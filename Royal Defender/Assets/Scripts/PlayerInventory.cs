using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject GunSocket;
    private Animator animator;
    private WeaponInfo currentRangedWeapon;
    private int currentRangedWeaponIndex;
    private WeaponInfo currentMeleeWeapon;
    private int currentMeleeWeaponIndex;
    private GameObject equippedWeapon;
    private ArrayList rangedWeapons;
    // TODO delete test value and implement real logic
    private bool rangedEquipped;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        currentRangedWeapon = null;
        currentRangedWeaponIndex = -1;

        currentMeleeWeapon = null;
        currentMeleeWeaponIndex = -1;

        equippedWeapon = null;

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
        if (rangedWeapons.Count <= 5)
        {
            rangedWeapons.Add(rangedWeapon);
            Debug.LogWarning("Ranged weapon count:" + rangedWeapons.Count + " - Player Inventory script.");
        }
    }

    private void SwitchRanged(int direction)
    {
        int desiredWeaponIndex = currentRangedWeaponIndex + direction;
        if (desiredWeaponIndex >=0 && desiredWeaponIndex <= rangedWeapons.Count - 1)
        {
            currentRangedWeapon = (WeaponInfo) rangedWeapons[desiredWeaponIndex];
            currentRangedWeaponIndex = desiredWeaponIndex;
        }
        else if (desiredWeaponIndex <= -1) // Unarmed
        {
            currentRangedWeapon = null;
            currentRangedWeaponIndex = -1;
        }
        
        EquipWeapon(currentRangedWeapon, WeaponType.Ranged);
        Debug.LogWarning("Switching to ranged weapon index: " + currentRangedWeaponIndex);
    }

    private void EquipWeapon(WeaponInfo weapon, WeaponType weaponType)
    {
        if (equippedWeapon)
        {
            UnequipWeapon();
        }

        if (weapon == null)
        {
            animator.SetBool("HasRangedWeapon", false);
            return;
        }

        GameObject spawnedWeapon = GameObject.Instantiate(weapon.weaponMesh);
        if (weaponType == WeaponType.Ranged)
        {
            animator.SetBool("HasRangedWeapon", true);
            spawnedWeapon.transform.parent = GunSocket.transform;
            spawnedWeapon.transform.localPosition = Vector3.zero;
            spawnedWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        equippedWeapon = spawnedWeapon;
    }

    private void UnequipWeapon()
    {
        Destroy(equippedWeapon);
    }
}
