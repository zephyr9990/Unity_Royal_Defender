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
    private WeaponInfo currentMeleeWeapon;
    private int currentMeleeWeaponIndex;
    private GameObject equippedWeapon;

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
        equippedWeapon = null;

        rangedWeapons = new ArrayList();

        // effects
        spawnEffectsPlaying = false;
        spawnEffectsTime = 0.3f;

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
            Debug.LogWarning("Ranged weapon count:" + rangedWeapons.Count + " - Player Inventory script.");
        }
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
            // Don't play effect nor spawn a new weapon.
            animator.SetBool("HasRangedWeapon", false);
            return;
        }

        GameObject spawnedWeapon = GameObject.Instantiate(weapon.weaponMesh);
        if (weaponType == WeaponType.Ranged)
        {
            animator.SetBool("HasRangedWeapon", true);
            spawnedWeapon.transform.parent = gunSocket.transform;
            spawnedWeapon.transform.localPosition = Vector3.zero;
            spawnedWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        equippedWeapon = spawnedWeapon;
        PlaySpawnEffects(gunSpawnParticles, gunSpawnLightEffect);
    }

    private void UnequipWeapon()
    {
        Destroy(equippedWeapon);
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
