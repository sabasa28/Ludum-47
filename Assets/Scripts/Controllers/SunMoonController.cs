using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SunMoonController : MonoBehaviour
{
    [SerializeField] SpriteRenderer moonLight = null;
    [SerializeField] SpriteRenderer sunLight = null;
    [SerializeField] Transform moon = null;
    [SerializeField] Transform sun = null;
    [SerializeField] float rotSpeed = 0;
    public Action <bool> OnStateChange;

    float distBetweenPlanets;
    float sunStartPosY;
    float moonStartPosY;
    Color moonLightCol;
    Color sunLightCol;

    bool shouldRotate = true;

    bool day = true;
    void Start()
    {
        moonStartPosY = moon.transform.position.y;
        sunStartPosY = sun.transform.position.y;
        moonLightCol = moonLight.color;
        sunLightCol = sunLight.color;
        distBetweenPlanets = Vector3.Distance(moon.position, sun.position); 
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (shouldRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotSpeed * Time.deltaTime);

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
            moonLight.color = Color.Lerp(new Color(moonLightCol.r, moonLightCol.g, moonLightCol.b, 0), new Color(moonLightCol.r, moonLightCol.g, moonLightCol.b, 1), tMoon);

            yield return null;
        }
    }
}
