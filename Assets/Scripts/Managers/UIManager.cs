using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float fadeDuration = 1f;

    public Image cover;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float currentAlpha = cover.color.a;

        while (currentAlpha > 0f)
        {
            float subtractedValue = 1f / (fadeDuration / Time.deltaTime);
            currentAlpha -= subtractedValue;

            Color newColor = cover.color;
            newColor.a = currentAlpha;
            cover.color = newColor;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float currentAlpha = cover.color.a;

        while (currentAlpha < 1f)
        {
            float addedValue = 1f / (fadeDuration / Time.deltaTime);
            currentAlpha += addedValue;

            Color newColor = cover.color;
            newColor.a = currentAlpha;
            cover.color = newColor;

            yield return null;
        }
    }
}