using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI : MonoBehaviour
{
    public Canvas npcCanvas;
    public Text nameText;
    public Text sliderText;
    public Slider reviveSlider;
    public string characterName = "";

    private void Start()
    {
        nameText.text = characterName;
    }

    // Update is called once per frame
    void Update()
    {
        npcCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    public void SetSliderValue(float value)
    {
        reviveSlider.value = value;
    }

    public void SetSliderText(string value)
    {
        sliderText.text = value;
    }
}
