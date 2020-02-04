using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePlate : MonoBehaviour
{
    public string characterName = "";

    private void Start()
    {
        GameObject namePlate = transform.GetChild(0).gameObject;
        namePlate.GetComponent<Text>().text = characterName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
