using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // sockets
    public GameObject gunSocket;
    
    // components
    private Animator animator;
    private ParticleSystem gunSpawnParticles;
    private Light gunSpawnLightEffect;
    
    // weapons
    private WeaponInfo currentRangedWeapon;
    private int currentRangedWeaponIndex;

    public int maxAmmo;
    public int startingAmmo;
    private int currentAmmo;

    private WeaponInfo currentMeleeWeapon;
    private int currentMeleeWeaponIndex;
    private GameObject equippedWeaponObject;
    private WeaponInfo equippedWeaponInfo;

    // inventory
    public int maxNumberOfRangedWeapons;
    private ArrayList rangedWeapons;

    // effects
    private float timer;
    private bool spawnEffectsPlaying;
    private float spawnEffectsTime;
    
    // Start is called before the first frame update
    void Start()
    {
        // components
        animator = GetComponentInChildren<Animator>();
        gunSpawnParticles = gunSocket.GetComponent<ParticleSystem>();
        gunSpawnLightEffect = gunSocket.GetComponent<Light>();

        // weapons
        currentRangedWeapon = null;
        currentRangedWeaponIndex = -1;
        currentMeleeWeapon = null;
        currentMeleeWeaponIndex = -1;        
        equippedWeaponObject = null;

        rangedWeapons = new ArrayList();

        // effects
        spawnEffectsPlaying = false;
        spawnEffectsTime = 0.3f;

        currentAmmo = startingAmmo;
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

        timer += Time.deltaTime;
        if (spawnEffectsPlaying && timer > spawnEffectsTime)
        {
            StopSpawnEffects(gunSpawnParticles, gunSpawnLightEffect);
        }
    }

    public void AddRangedWeapon(WeaponInfo rangedWeapon)
    {
        // limit number of ranged weapons to 5
        if (rangedWeapons.Count < maxNumberOfRangedWeapons)
        {
            rangedWeapons.Add(rangedWeapon);
        }
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
        // If these is already a weapon being displayed, unequip it before a new object is displayed
        if (equippedWeaponObject)
        {
            UnequipWeapon();
        }

        // If no weapon is given, player wants to be unarmed
        if (weapon == null)
        {
            // Don't play effect nor spawn a new weapon.
            animator.SetBool("HasRangedWeapon", false);
            equippedWeaponInfo = weapon;
            return;
        }

        GameObject spawnedWeapon = GameObject.Instantiate(weapon.weaponMesh);
        if (weapon.weaponType == WeaponType.Ranged)
        {
            animator.SetBool("HasRangedWeapon", true);
            spawnedWeapon.transform.parent = gunSocket.transform;
            spawnedWeapon.transform.localPosition = Vector3.zero;
            spawnedWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        equippedWeaponInfo = weapon;
        equippedWeaponObject = spawnedWeapon;
        PlaySpawnEffects(gunSpawnParticles, gunSpawnLightEffect);
    }

    private void UnequipWeapon()
    {
        Destroy(equippedWeaponObject);
    }

    public WeaponInfo getEquippedWeaponInfo()
    {
        return equippedWeaponInfo;
    }

    private void PlaySpawnEffects(ParticleSystem particles, Light light)
    {
        timer = 0f;
        spawnEffectsPlaying = true;
        particles.Stop();
        particles.Play();
        light.enabled = true;
    }

    private void StopSpawnEffects(ParticleSystem particles, Light light)
    {
        particles.Stop();
        light.enabled = false;
        spawnEffectsPlaying = false;   
    }
}
