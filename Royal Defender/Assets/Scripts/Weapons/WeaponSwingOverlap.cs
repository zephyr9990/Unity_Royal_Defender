using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwingOverlap : MonoBehaviour
{
    private GameObject owner;
    public float force = 10f;
    private PlayerEquippedWeapon playerEquippedWeapon;
    private NPCEquippedWeapon npcEquippedWeapon;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            if (owner.CompareTag("Player"))
            {
                PlayerAttackEnemy(other, playerEquippedWeapon);
            }
            else // npc attacking
            {
                NPCAttackEnemy(other, npcEquippedWeapon);
            }
        }
    }

    private void PlayerAttackEnemy(Collider other, PlayerEquippedWeapon equippedWeapon)
    {
        equippedWeapon.SwingAt(other.gameObject);
        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            Vector3 pushBackDirection = other.transform.position - owner.transform.position;
            other.gameObject.GetComponent<PushBack>().AddPushBack(pushBackDirection.normalized, force);
        }
    }

    private void NPCAttackEnemy(Collider other, NPCEquippedWeapon equippedWeapon)
    {
        equippedWeapon.SwingAt(other.gameObject);
        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            Vector3 pushBackDirection = other.transform.position - owner.transform.position;
            other.gameObject.GetComponent<PushBack>().AddPushBack(pushBackDirection.normalized, force);
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
        if (owner.CompareTag("Player"))
        {
            playerEquippedWeapon = owner.GetComponent<PlayerEquippedWeapon>();
        }
        else // is npc
        {
            npcEquippedWeapon = owner.GetComponent<NPCEquippedWeapon>();
        }
    }
}
