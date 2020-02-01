using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private ParticleSystem meleeParticleEffect;
    private Light meleeLightEffect;
    private bool weaponSwitchEffectPlaying;

    // components
    private WeaponInfo equippedWeapon;
    private Animator animator;

    // ui
    public RawImage MeleeWeaponImage;
    public Text MeleeWeaponName;
    public Text MeleeWeaponDamage;
    public Slider MeleeWeaponSlider;

    public RawImage RangedWeaponImage;
    public Text RangedWeaponName;
    public Text RangedWeaponDamage;
    public Slider RangedWeaponSlider;

    // Start is called before the first frame update
    void Start()
    {
        effectsDisplayTime = 0.3f;
        weaponSwitchEffectPlaying = false;
        rangedParticleEffect = rangedSocket.GetComponent<ParticleSystem>();
        rangedLightEffect = rangedSocket.GetComponent<Light>();
        meleeParticleEffect = meleeSocket.GetComponent<ParticleSystem>();
        meleeLightEffect = meleeSocket.GetComponent<Light>();

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
        // If there is already a weapon being displayed, unequip it before a new object is displayed
        if (equippedWeapon != null)
        {
            UnequipWeapon();
        }

        // If no weapon is given, player wants to be unarmed
        if (weapon == null)
        {
            // Don't play effect nor spawn a new weapon.
            animator.SetBool("HasRangedWeapon", false);
            animator.SetBool("HasMeleeWeapon", false);

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

    public GameObject GetEquippedMeleeWeaponObject()
    {
        return meleeSocket.transform.GetChild(0).gameObject;
    }

    private void AttachToWeaponSocket(WeaponInfo weapon)
    {
        GameObject weaponToEquip = GameObject.Instantiate(weapon.weaponMesh);
        GameObject socket;
        if (weapon.type == WeaponType.Ranged)
        {
            animator.SetBool("HasMeleeWeapon", false);

            animator.SetBool("HasRangedWeapon", true);
            socket = rangedSocket;
        }
        else // type == melee
        {
            animator.SetBool("HasMeleeWeapon", true);

            animator.SetBool("HasRangedWeapon", false);
            socket = meleeSocket;
        }

        weaponToEquip.transform.parent = socket.transform;
        weaponToEquip.transform.localPosition = Vector3.zero;
        weaponToEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);
        PlayWeaponSwitchEffects(weapon);
        UpdateWeaponUI(weapon);

        // TODO add melee logic
    }

    private void UpdateWeaponUI(WeaponInfo weapon)
    {
        if (weapon == null)
        {
            // TODO set null values for equipped weapon type
            return;
        }

        if (weapon.type == WeaponType.Melee)
        {
            MeleeWeaponImage.texture = weapon.icon;
            MeleeWeaponName.text = weapon.weaponName;
            MeleeWeaponDamage.text = weapon.damage + " DMG";
            MeleeWeaponSlider.value = weapon.getWeaponDurabilityPercent();
        }
        else // ranged
        {
            RangedWeaponImage.texture = weapon.icon;
            RangedWeaponName.text = weapon.weaponName;
            RangedWeaponDamage.text = weapon.damage + " DMG";
            RangedWeaponSlider.value = weapon.getWeaponDurabilityPercent();
        }
    }

    public void Shoot(GameObject enemy)
    {
        if (enemy)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(equippedWeapon.damage);
        }
    }

    public void SwingAt(GameObject enemy)
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
            rangedLightEffect.enabled = false;
            rangedLightEffect.enabled = true;
        }
        else // melee type weapon
        {
            meleeParticleEffect.Stop();
            meleeParticleEffect.Play();
            meleeLightEffect.enabled = false;
            meleeLightEffect.enabled = true;
        }

        weaponSwitchEffectPlaying = true;
    }

    private void StopWeaponSwitchEffects()
    {
        weaponSwitchEffectPlaying = false;
        // Turn off all effects if player switches weapon type or goes unarmed
        if (equippedWeapon == null)
        {
            rangedParticleEffect.Stop();
            rangedLightEffect.enabled = false;
            meleeParticleEffect.Stop();
            meleeLightEffect.enabled = false;
            return;

        }

        if (equippedWeapon.type == WeaponType.Ranged)
        {
            rangedParticleEffect.Stop();
            rangedLightEffect.enabled = false;
        }
        else if (equippedWeapon.type == WeaponType.Melee)
        {
            meleeParticleEffect.Stop();
            meleeLightEffect.enabled = false;
        }
    }
}
