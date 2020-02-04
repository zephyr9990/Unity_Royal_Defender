using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwingOverlap : MonoBehaviour
{
    public float force = 10f;
    private GameObject player;
    private GameObject npc;
    private PlayerEquippedWeapon equippedWeapon;
    private NPCEquippedWeapon npcEquippedWeapon;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        equippedWeapon = player.GetComponent<PlayerEquippedWeapon>();

        npc = GameObject.FindGameObjectWithTag("NPC");
        npcEquippedWeapon = npc.GetComponent<NPCEquippedWeapon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (transform.root.CompareTag("Player"))
            {
                PlayerAttackEnemy(other, equippedWeapon);
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
            Vector3 pushBackDirection = other.transform.position - player.transform.position;
            other.gameObject.GetComponent<PushBack>().AddPushBack(pushBackDirection.normalized, force);
        }
    }

    private void NPCAttackEnemy(Collider other, NPCEquippedWeapon equippedWeapon)
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
