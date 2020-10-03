using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonController : MonoBehaviour
{
    [SerializeField] SpriteRenderer moonLight;
    [SerializeField] SpriteRenderer sunLight;
    [SerializeField] Transform moon;
    [SerializeField] Transform sun;
    [SerializeField] float rotSpeed;

    float distBetweenPlanets;
    float sunStartPosY;
    float moonStartPosY;
    Color moonLightCol;
    Color sunLightCol;

    bool shouldRotate = true;
    void Start()
    {
        moonLightCol = moonLight.color;
        sunLightCol = sunLight.color;
        StartCoroutine(Rotate());
        distBetweenPlanets = Vector3.Distance(moon.position, sun.position); 
    }

    IEnumerator Rotate()
    {
        while (shouldRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotSpeed * Time.deltaTime);

            float tMoon = (moon.position.y - moonStartPosY + (distBetweenPlanets / 2)) / distBetweenPlanets;
            float tSun = (sun.position.y - sunStartPosY + (distBetweenPlanets / 2)) / distBetweenPlanets;

            
            sunLight.color = Color.Lerp(new Color(sunLightCol.r, sunLightCol.g, sunLightCol.b, 0), new Color(sunLightCol.r, sunLightCol.g, sunLightCol.b, 1), tSun);
            moonLight.color = Color.Lerp(new Color(moonLightCol.r, moonLightCol.g, moonLightCol.b, 0), new Color(moonLightCol.r, moonLightCol.g, moonLightCol.b, 1), tMoon);

            yield return null;
        }
    }
}
