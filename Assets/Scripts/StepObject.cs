using System.Collections;
using UnityEngine;

public class StepObject : MonoBehaviour
{
    SpriteRenderer sr;
    float timeToFadeIn = 1.0f;
    float timeToFadeOut = 1.0f;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();    
    }
    public void Deactivate()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(FadeAndDeactivate());
    }
    public void Appear()
    {
        StartCoroutine(ActivateAndFadeIn());
    }
    IEnumerator FadeAndDeactivate()
    {
        float t = 0;
        Color visibleCol = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        Color notVisibleCol = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        while (t < 1)
        {
            t += Time.deltaTime / timeToFadeOut;
            sr.color = Color.Lerp(visibleCol,notVisibleCol,t);
            yield return null;
        }
        gameObject.SetActive(false);
    }
    IEnumerator ActivateAndFadeIn()
    {
        float t = 0;
        Color visibleCol = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        Color notVisibleCol = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        while (t < 1)
        {
            t += Time.deltaTime / timeToFadeIn;
            sr.color = Color.Lerp(notVisibleCol, visibleCol, t);
            yield return null;
        }
    }
}