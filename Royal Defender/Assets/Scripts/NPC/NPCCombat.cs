using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCombat : MonoBehaviour
{
    public float timeBetweenShots = 0.15f;
    public float timeBetweenSwings = .6f;
    public float range = 100.0f;
    public int numberOfShotsPerBurst = 15;
    public float timeBetweenBursts = 3f;
    public GameObject rangedSocket;

    private float effectsDisplayTime = .2f;
    private float timer = 0;
    private bool InMeleeRange;
    private bool InShootingRange;
    private bool isCoolingDownFromBurst;

    private int numberOfShotsFired;

    private Animator animator;
    private NPCEquippedWeapon npcEquippedWeapon;
    private LockOnScript lockOn;

    // Start is called before the first frame update
    private void Awake()
    {
        numberOfShotsFired = 0;
        InMeleeRange = false;
        InShootingRange = false;
        isCoolingDownFromBurst = false;
        animator = GetComponentInChildren<Animator>();
        npcEquippedWeapon = GetComponent<NPCEquippedWeapon>();
        lockOn = GetComponentInChildren<LockOnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        WeaponInfo currentWeapon = npcEquippedWeapon.GetWeaponInfo();

        if (currentWeapon == null)
        {
            StopAttackingAnimations();
            return;
        }

        if (currentWeapon.type == WeaponType.Ranged)
        {
            CheckIfNPCWantsToShoot();
        }
        else // player has melee weapon
        {
            CheckIfNPCWantsToSwing();
        }


    }

    private void CheckIfNPCWantsToSwing()
    {
        if (InMeleeRange)
        {
            bool isSprinting = animator.GetBool("IsSprinting");
            if (isSprinting)
                return; // cannot swing weapon if sprinting;

            if (timer > timeBetweenSwings)
            {
                timer = 0f;
                animator.SetBool("IsSwinging", false);
                animator.SetBool("IsSwinging", true);
            }
        }
    }

    private void CheckIfNPCWantsToShoot()
    {
        if (InShootingRange)
        {
            if (timer > timeBetweenShots
                && animator.GetBool("LockOnToggled"))
            {
                if (!IsOnCooldownFromBurst())
                {
                    Shoot();
                }
            }
            else
            {
                //TODO check if this is truly needed.
                StopAttackingAnimations();
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

    private void Shoot()
    {
        timer = 0.0f;
        animator.SetBool("IsShooting", true);

        // Player can change weapons, so must be actively called for different weapons
        AudioSource gunAudio = rangedSocket.GetComponentInChildren<AudioSource>();
        ParticleSystem muzzleFlash = rangedSocket.transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        Light muzzleLight = rangedSocket.transform.GetChild(0).GetComponentInChildren<Light>();

        muzzleFlash.Play();
        muzzleLight.enabled = true;

        gunAudio.pitch = UnityEngine.Random.Range(.99f, 1.01f);
        gunAudio.Play();

        // Damage enemy that is currently locked on.
        GameObject enemy = transform.GetChild(0).GetComponent<NPCAttackRange>().GetNearestTarget();
        if (enemy)
        {
            npcEquippedWeapon.Shoot(enemy);
        }

        if (++numberOfShotsFired > numberOfShotsPerBurst)
        {
            isCoolingDownFromBurst = true;
            return;
        }
        isCoolingDownFromBurst = false;
    }

    private bool IsOnCooldownFromBurst()
    {
        if (timer > timeBetweenBursts)
        {
            isCoolingDownFromBurst = false;
            numberOfShotsFired = 0;
        }
        return isCoolingDownFromBurst;
    }

    private void StopShooting()
    {
        Light muzzleLight = rangedSocket.transform.GetChild(0).GetComponentInChildren<Light>();
        muzzleLight.enabled = false;
        animator.SetBool("IsShooting", false);
    }

    public void StopAttackingAnimations()
    {
        animator.SetBool("IsShooting", false);
    }

    private void StopEffects()
    {
        Light muzzleLight = rangedSocket.transform.GetChild(0).GetComponentInChildren<Light>();
        muzzleLight.enabled = false;
    }

    public void SetInMeleeRange(bool value)
    {
        InMeleeRange = value;
    }

    public void SetInShootingRange(bool value)
    {
        InShootingRange = value;
    }
}

