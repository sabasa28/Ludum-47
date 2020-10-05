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
    [SerializeField] Transform mandalaPlanet = null;
    [SerializeField] SpriteRenderer mandalaPlanetSR = null;
    [SerializeField] Image mandala= null;
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
        mandalaPlanet.position = sun.position;

        StartCoroutine(FinalAnimation(mandalaPlanetSR, mandala));
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

    IEnumerator FinalAnimation(SpriteRenderer planetSR, Image mandala)
    {
        float t = 0;
        Color visibleCol = new Color(planetSR.color.r, planetSR.color.g, planetSR.color.b, 1);
        Color notVisibleCol = new Color(planetSR.color.r, planetSR.color.g, planetSR.color.b, 0);
        while (t < 1)
        {
            t += Time.deltaTime / fadeDuration;
            planetSR.color = Color.Lerp(notVisibleCol, visibleCol, t);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        t = 0;
        visibleCol = new Color(mandala.color.r, mandala.color.g, mandala.color.b, 1);
        notVisibleCol = new Color(mandala.color.r, mandala.color.g, mandala.color.b, 0);
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
