using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeSpeed = 3f;
    public RawImage frontMostFaderImage;
    private RawImage faderImage;
    private bool fadingToBlack;
    private bool fadingToClear;
    private bool fadingToWhite;
    private float fadeAmount;
    private float timer;

    private void Awake()
    {
        faderImage = GetComponent<RawImage>();
        faderImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        fadingToBlack = false;
        fadingToClear = false;
        fadingToWhite = false;
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

       if (fadingToWhite && timer >= fadeSpeed)
        {
            BrightenScreen();
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

    private void BrightenScreen()
    {
        timer = 0f;
        fadeAmount += Time.deltaTime;
        if (faderImage.color.a == 1f)
        {
            fadeAmount = 0;
            fadingToWhite = false;
            return;
        }

        faderImage.color = Color.Lerp(faderImage.color, Color.white, fadeAmount);
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

    public void SkipScene()
    {
        faderImage = frontMostFaderImage;
        FadeToBlack();
    }
}
