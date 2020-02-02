using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwingOverlap : MonoBehaviour
{
    public float force = 10f;
    private GameObject player;
    private PlayerEquippedWeapon equippedWeapon;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        equippedWeapon = player.GetComponent<PlayerEquippedWeapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            equippedWeapon.SwingAt(other.gameObject);
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (rigidbody)
            {
                Vector3 pushBackDirection = other.transform.position - player.transform.position;
                other.gameObject.GetComponent<PushBack>().AddPushBack(pushBackDirection.normalized, force);
            }
        }
    }
}
