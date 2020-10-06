using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SunMoonController : MonoBehaviour
{
    [HideInInspector] public bool moonShouldRotate = true;
    [HideInInspector] public bool sunShouldRotate = true;
    [HideInInspector] public bool firstPlanetCollided = false;

    [SerializeField] float fadeDuration = 3f;

    [SerializeField] Transform moonPivot = null;
    [SerializeField] Transform sunPivot = null;
    [SerializeField] SpriteRenderer moonLight = null;
    [SerializeField] SpriteRenderer sunLight = null;
    [SerializeField] Image nightBG = null;
    [SerializeField] Image dayBG = null;
    [SerializeField] Transform moon = null;
    [SerializeField] Transform sun = null;
    [SerializeField] Transform mandalaPlanet1 = null;
    [SerializeField] SpriteRenderer mandalaPlanet1SR = null;
    [SerializeField] Transform mandalaPlanet2 = null;
    [SerializeField] SpriteRenderer mandalaPlanet2SR = null;
    [SerializeField] Image mandala = null;
    [SerializeField] float rotSpeed = 0;
    public Action <bool> OnStateChange;

    float distBetweenPlanets;
    float sunStartPosY;
    float moonStartPosY;
    Color moonLightCol;
    Color sunLightCol;

    bool shouldRotate = true;

    bool day = true;

    public static event Action OnFinalAnimationFinished;

    void Start()
    {
        moonStartPosY = moon.transform.position.y;
        sunStartPosY = sun.transform.position.y;
        moonLightCol = moonLight.color;
        sunLightCol = sunLight.color;
        distBetweenPlanets = Vector3.Distance(moon.position, sun.position); 
        StartCoroutine(Rotate());
    }

    public void StartFinalAnimation()
    {
        mandalaPlanet1.position = sun.position;
        mandalaPlanet2.position = moon.position;

        StartCoroutine(FinalAnimation(mandalaPlanet1SR, mandalaPlanet2SR, mandala));
    }

    IEnumerator Rotate()
    {
        while (shouldRotate)
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotSpeed * Time.deltaTime);
            if (moonShouldRotate)
                moonPivot.rotation = Quaternion.Euler(moonPivot.rotation.eulerAngles.x, moonPivot.rotation.eulerAngles.y, moonPivot.rotation.eulerAngles.z + rotSpeed * Time.deltaTime);

            if (sunShouldRotate)
                sunPivot.rotation = Quaternion.Euler(sunPivot.rotation.eulerAngles.x, sunPivot.rotation.eulerAngles.y, sunPivot.rotation.eulerAngles.z + rotSpeed * Time.deltaTime);

            float tMoon = (moon.position.y - moonStartPosY + (distBetweenPlanets / 2)) / distBetweenPlanets;
            float tSun = (sun.position.y - sunStartPosY + (distBetweenPlanets / 2)) / distBetweenPlanets;

            if (day && tMoon > 0.5f)
            {
                day = false;
                OnStateChange(day);
            }
            if (!day && tMoon < 0.5f)
            {
                day = true;
                OnStateChange(day);
            }

            sunLight.color = Color.Lerp(new Color(sunLightCol.r, sunLightCol.g, sunLightCol.b, 0), new Color(sunLightCol.r, sunLightCol.g, sunLightCol.b, 1), tSun);
            dayBG.color = Color.Lerp(new Color(1f, 1f, 1f, 0), new Color(1f, 1f, 1f, 1), tSun);

            moonLight.color = Color.Lerp(new Color(moonLightCol.r, moonLightCol.g, moonLightCol.b, 0), new Color(moonLightCol.r, moonLightCol.g, moonLightCol.b, 1), tMoon);
            nightBG.color = Color.Lerp(new Color(1f, 1f, 1f, 0), new Color(1f, 1f, 1f, 1), tMoon);

            yield return null;
        }
    }

    IEnumerator FinalAnimation(SpriteRenderer planet1SR, SpriteRenderer planet2SR, Image mandala)
    {
        float t = 0;
        Color visibleCol1 = new Color(planet1SR.color.r, planet1SR.color.g, planet1SR.color.b, 1);
        Color notVisibleCol1 = new Color(planet1SR.color.r, planet1SR.color.g, planet1SR.color.b, 0);

        Color visibleCol2 = new Color(planet2SR.color.r, planet2SR.color.g, planet2SR.color.b, 1);
        Color notVisibleCol2 = new Color(planet2SR.color.r, planet2SR.color.g, planet2SR.color.b, 0);

        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            planet1SR.color = Color.Lerp(notVisibleCol1, visibleCol1, t);
            planet2SR.color = Color.Lerp(notVisibleCol2, visibleCol2, t);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        t = 0;
        Color visibleCol = new Color(mandala.color.r, mandala.color.g, mandala.color.b, 1);
        Color notVisibleCol = new Color(mandala.color.r, mandala.color.g, mandala.color.b, 0);
        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            mandala.color = Color.Lerp(notVisibleCol, visibleCol, t);
            yield return null;
        }

        yield return new WaitForSeconds(5f);

        OnFinalAnimationFinished?.Invoke();

        yield return new WaitForSeconds(10f);

        Application.Quit();
    }
}
