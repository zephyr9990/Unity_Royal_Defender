using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, IRangedWeapon
{
    public void Fire(Animator shooterAnimator)
    {
        shooterAnimator.SetBool("IsShooting", true);

        AudioSource gunAudio = GetComponent<AudioSource>();
        ParticleSystem muzzleFlash = GetComponentInChildren<ParticleSystem>();

        muzzleFlash.Play();

        gunAudio.pitch = UnityEngine.Random.Range(.99f, 1.01f);
        gunAudio.Play();
    }

    public void StopFiring()
    {

    }
}
