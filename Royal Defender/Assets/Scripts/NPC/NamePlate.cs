using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePlate : MonoBehaviour
{
    public string name = "";

    private void Start()
    {
        GameObject namePlate = transform.GetChild(0).gameObject;
        namePlate.GetComponent<Text>().text = name;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
