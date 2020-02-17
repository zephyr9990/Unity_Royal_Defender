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

    private void Awake()
    {
        faderImage = GetComponent<RawImage>();
        faderImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        fadingToBlack = false;
        fadingToClear = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (fadingToBlack)
        {
            DarkenScreen();
        }

       if (fadingToClear)
        {
            ClearScreen();
        }
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
        if (faderImage.color.a == 1f)
        { 
            fadingToBlack = false;
            return;
        }

        faderImage.color = Color.Lerp(faderImage.color, Color.black, Time.deltaTime * fadeSpeed);
    }

    private void ClearScreen()
    {
        if (faderImage.color.a == 0f)
        {
            fadingToClear = false;
            return;
        }

        faderImage.color = Color.Lerp(faderImage.color, Color.clear, Time.deltaTime * fadeSpeed);
    }
}
