using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed = 3f;
    private RawImage faderImage;
    private bool fadingToBlack;
    private bool fadingToClear;
    private float fadeAmount;
    private float timer;

    private void Awake()
    {
        faderImage = GetComponent<RawImage>();
        faderImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        fadingToBlack = false;
        fadingToClear = false;
        fadeAmount = 0f;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
       if (fadingToBlack && timer >= fadeSpeed)
       {
            DarkenScreen();
       }

       if (fadingToClear && timer >= fadeSpeed)
       {
            ClearScreen();
       }
    }

    public void SetColorToWhite()
    {
        faderImage.color = Color.white;
    }

    public void SetColorToBlack()
    {
        faderImage.color = Color.black;
    }

    public void SetAlphaToZero()
    {
        Color alphaZero = faderImage.color;
        alphaZero.a = 0f;
        faderImage.color = alphaZero;
    }

    public void FadeToBlack()
    {
        fadingToBlack = true;
    }

    public void FadeToClear()
    {
        fadingToClear = true;
    }

    private void DarkenScreen()
    {
        timer = 0f;
        fadeAmount += Time.deltaTime;
        if (faderImage.color.a == 1f)
        {
            fadeAmount = 0;
            fadingToBlack = false;
            return;
        }

        faderImage.color = Color.Lerp(faderImage.color, Color.black, fadeAmount);
    }

    private void ClearScreen()
    {
        timer = 0f;
        fadeAmount += Time.deltaTime;
        if (faderImage.color.a == 0f)
        {
            fadeAmount = 0;
            fadingToClear = false;
            return;
        }

        faderImage.color = Color.Lerp(faderImage.color, Color.clear, fadeAmount);
    }
}
