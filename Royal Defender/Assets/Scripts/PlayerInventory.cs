using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Animator animator;
    
    // TODO delete test value and implement real logic
    private bool rangedEquipped;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        // TODO delete test value and implement real logic
        rangedEquipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Test logic only. Implement real logic

        if (Input.GetButtonDown("SwitchRanged"))
        {
            Debug.LogError("Firing");
            rangedEquipped = !rangedEquipped;
            animator.SetBool("HasRangedWeapon", rangedEquipped);
            GameObject gun = gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
            gun.SetActive(rangedEquipped); 
        }
    }
}
