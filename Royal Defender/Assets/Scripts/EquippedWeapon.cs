using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedWeapon : MonoBehaviour
{
    // weapon sockets
    public GameObject rangedSocket;
    public GameObject meleeSocket;

    // effects
    private float timer;
    private float effectsDisplayTime;
    private ParticleSystem rangedParticleEffect;
    private Light rangedLightEffect;
    private bool weaponSwitchEffectPlaying;

    // other
    private WeaponInfo equippedWeapon;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        effectsDisplayTime = 0.3f;
        weaponSwitchEffectPlaying = false;
        rangedParticleEffect = rangedSocket.GetComponent<ParticleSystem>();
        rangedLightEffect = rangedSocket.GetComponent<Light>();

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (weaponSwitchEffectPlaying && timer >= effectsDisplayTime)
        {
            StopWeaponSwitchEffects();
        }
    }

    public void EquipWeapon(WeaponInfo weapon)
    {
        // If these is already a weapon being displayed, unequip it before a new object is displayed
        if (equippedWeapon != null)
        {
            UnequipWeapon();
        }

        // If no weapon is given, player wants to be unarmed
        if (weapon == null)
        {
            // Don't play effect nor spawn a new weapon.
            animator.SetBool("HasRangedWeapon", false);
            UnequipWeapon();
            return;
        }

        AttachToWeaponSocket(weapon);
        equippedWeapon = weapon;
    }

    public void UnequipWeapon()
    {
        if (equippedWeapon == null)
            return;

        if (equippedWeapon.type == WeaponType.Ranged)
        {
            GameObject rangedWeapon = rangedSocket.transform.GetChild(0).gameObject;
            Destroy(rangedWeapon);
        }
        else if (equippedWeapon.type == WeaponType.Melee)
        {
            GameObject meleeWeapon = meleeSocket.transform.GetChild(0).gameObject;
            Destroy(meleeWeapon);
        }

        equippedWeapon = null;
    }

    public WeaponInfo GetWeaponInfo()
    {
        return equippedWeapon;
    }

    private void AttachToWeaponSocket(WeaponInfo weapon)
    {
        GameObject weaponToEquip = GameObject.Instantiate(weapon.weaponMesh);
        if (weapon.type == WeaponType.Ranged)
        {
            animator.SetBool("HasRangedWeapon", true);
            weaponToEquip.transform.parent = rangedSocket.transform;
            weaponToEquip.transform.localPosition = Vector3.zero;
            weaponToEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);
            PlayWeaponSwitchEffects(weapon);
        }

        // TODO add melee logic
    }

    public void Shoot(GameObject enemy)
    {
        if (enemy)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(equippedWeapon.damage);
        }
    }

    private void PlayWeaponSwitchEffects(WeaponInfo weapon)
    {
        timer = 0.0f;
        if (weapon.type == WeaponType.Ranged)
        {
            rangedParticleEffect.Stop();
            rangedParticleEffect.Play();
            rangedLightEffect.enabled = true;
        }
        else // melee type weapon
        {
            // TODO melee effect spawn
        }

        weaponSwitchEffectPlaying = true;
    }

    private void StopWeaponSwitchEffects()
    {
        weaponSwitchEffectPlaying = false;
        rangedParticleEffect.Stop();
        rangedLightEffect.enabled = false;
    }
}
