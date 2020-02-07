using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePlate : MonoBehaviour
{
    public Text nameText;
    public string characterName = "";

    private void Start()
    {
        nameText.text = characterName;
    }

    // Update is called once per frame
    void Update()
    {
        nameText.transform.rotation = Camera.main.transform.rotation;
    }
}
