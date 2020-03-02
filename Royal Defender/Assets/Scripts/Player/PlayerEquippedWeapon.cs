using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquippedWeapon : MonoBehaviour
{
    // weapon
    public GameObject rangedSocket;
    public GameObject meleeSocket;
    public Texture UnarmedIcon;
    private int durabilityDecreaseAmount;

    // effects
    private float timer;
    private float effectsDisplayTime;
    private ParticleSystem rangedParticleEffect;
    private Light rangedLightEffect;
    private ParticleSystem meleeParticleEffect;
    private Light meleeLightEffect;
    private bool weaponSwitchEffectPlaying;

    // components
    public PlayerInventory playerInventory;
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

    private void Awake()
    {
        durabilityDecreaseAmount = 1;
        effectsDisplayTime = 0.3f;
        weaponSwitchEffectPlaying = false;
        rangedParticleEffect = rangedSocket.GetComponent<ParticleSystem>();
        rangedLightEffect = rangedSocket.GetComponent<Light>();
        meleeParticleEffect = meleeSocket.GetComponent<ParticleSystem>();
        meleeLightEffect = meleeSocket.GetComponent<Light>();

        animator = GetComponent<Animator>();
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

        StopWeaponSwitchEffects();

        // If no weapon is given, player wants to be unarmed
        if (weapon == null)
        {
            // Don't play effect nor spawn a new weapon.
            UnequipWeapon();
            return;
        }

        AttachToWeaponSocket(weapon);
        equippedWeapon = weapon;
    }

    public void UnequipWeapon()
    {
        if (equippedWeapon == null)
        {   
            SetUnarmedValues(playerInventory.GetCurrentType());
            return;
        }

        SetUnarmedValues(equippedWeapon.type);

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

        animator.SetBool("HasRangedWeapon", false);
        animator.SetBool("HasMeleeWeapon", false);
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
        GameObject weaponToEquip = GameObject.Instantiate(weapon.mesh);
        GameObject socket;
        if (weapon.type == WeaponType.Ranged)
        {
            animator.SetBool("IsSwinging", false);
            animator.SetBool("HasMeleeWeapon", false);
            animator.SetBool("HasRangedWeapon", true);
            socket = rangedSocket;
        }
        else // type == melee
        {
            animator.SetBool("HasMeleeWeapon", true);
            animator.SetBool("HasRangedWeapon", false);
            weaponToEquip.GetComponent<WeaponSwingOverlap>().SetOwner(gameObject);
            socket = meleeSocket;
        }

        weaponToEquip.transform.parent = socket.transform;
        weaponToEquip.transform.localPosition = Vector3.zero;
        weaponToEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);

        PlayWeaponSwitchEffects(weapon);
        UpdateWeaponUI(weapon);
    }

    private void UpdateWeaponUI(WeaponInfo weapon)
    {
        if (weapon == null)
        {
            return;
        }

        if (weapon.type == WeaponType.Melee)
        {
            MeleeWeaponImage.texture = weapon.icon;
            MeleeWeaponName.text = weapon.name;
            MeleeWeaponDamage.text = weapon.damage + " DMG";
            MeleeWeaponSlider.gameObject.SetActive(true);
            MeleeWeaponSlider.value = weapon.GetDurabilityPercent();
        }
        else // ranged
        {
            RangedWeaponImage.texture = weapon.icon;
            RangedWeaponName.text = weapon.name;
            RangedWeaponDamage.text = weapon.damage + " DMG";
            RangedWeaponSlider.gameObject.SetActive(true);
            RangedWeaponSlider.value = weapon.GetDurabilityPercent();
        }
    }

    public void Shoot(GameObject enemy)
    {
        if (enemy)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(equippedWeapon.damage);
        }

        DecreaseDurability(durabilityDecreaseAmount);
        UpdateWeaponUI(equippedWeapon);
    }

    public void SwingAt(GameObject enemy)
    {
        if (enemy)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(equippedWeapon.damage);
        }

        DecreaseDurability(durabilityDecreaseAmount);
        UpdateWeaponUI(equippedWeapon);
    }

    private void DecreaseDurability(int amount)
    {
        equippedWeapon.DecreaseDurability(amount);
        if (equippedWeapon.GetCurrentDurability() <= 0)
        {
            DestroyWeapon();
        }
    }

    public void DestroyWeapon()
    {
          playerInventory.DiscardWeapon(equippedWeapon.type);
          UnequipWeapon();
    }

    private void SetUnarmedValues(WeaponType type)
    {

        if (type == WeaponType.Ranged)
        {
            RangedWeaponImage.texture = UnarmedIcon;
            RangedWeaponName.text = "Unarmed";
            RangedWeaponDamage.text = "";
            RangedWeaponSlider.gameObject.SetActive(false);
        }
        else // melee
        {
            MeleeWeaponImage.texture = UnarmedIcon;
            MeleeWeaponName.text = "Unarmed";
            MeleeWeaponDamage.text = "";
            MeleeWeaponSlider.gameObject.SetActive(false);
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
