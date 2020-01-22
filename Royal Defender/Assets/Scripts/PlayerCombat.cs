using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float timeBetweenShots = 0.15f;
    public float range = 100.0f;
    public GameObject gunSocket;

    private int shootableMask;
    private float effectsDisplayTime = .2f;
    private PlayerInventory playerInventory;
    private Animator animator;
    private float timer = 0;
    private EquippedWeapon equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        animator = GetComponentInChildren<Animator>();
        playerInventory = GetComponent<PlayerInventory>();
        equippedWeapon = GetComponent<EquippedWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        WeaponInfo currentWeapon = equippedWeapon.GetWeaponInfo();

        if (currentWeapon == null)
        {
            StopAttackingAnimations();
            return;
        }

        if (currentWeapon.type == WeaponType.Ranged)
        {
            if (Input.GetButton("Attack"))
            {
                if (timer > timeBetweenShots
                    && animator.GetBool("LockOnToggled"))
                {
                    Shoot();
                }
            }

            if (timer >= timeBetweenShots * effectsDisplayTime)
            {
                StopEffects();
            }

            if (Input.GetButtonUp("Attack"))
            {
                StopShooting();
            }
        }

        
    }

    private void Shoot()
    {
        timer = 0.0f;
        animator.SetBool("IsShooting", true);

        // Player can change weapons, so must be actively called for different weapons
        AudioSource gunAudio = gunSocket.GetComponentInChildren<AudioSource>();
        ParticleSystem muzzleFlash = gunSocket.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        Light muzzleLight = gunSocket.transform.GetChild(0).GetComponentInChildren<Light>();

        muzzleFlash.Play();
        muzzleLight.enabled = true;

        gunAudio.pitch = Random.Range(1.0f, 1.1f);
        gunAudio.Play();
        // TODO Spawn shooting projectile. Spawn 3 for single press
    }

    private void StopShooting()
    {
        Light muzzleLight = gunSocket.transform.GetChild(0).GetComponentInChildren<Light>();
        muzzleLight.enabled = false;
        animator.SetBool("IsShooting", false);
    }

    public void StopAttackingAnimations()
    {
        animator.SetBool("IsShooting", false);
    }

    private void StopEffects()
    {
        Light muzzleLight = gunSocket.transform.GetChild(0).GetComponentInChildren<Light>();
        muzzleLight.enabled = false;
    }
}
