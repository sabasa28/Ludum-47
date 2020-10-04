using System;
using System.Collections;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    int timesGrown = 0;

    [SerializeField] float growFactor = 1.2f;

    [SerializeField] RainAnimation rain = null;
    Transform collidingCloud;

    public Draggable[] clouds;

    static public Action OnCloudCompleted;

    void Awake()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].Reaction += GrowCloud;
        }
    }

    void GrowCloud()
    {
        timesGrown++;
        transform.localScale *= growFactor;

        if (timesGrown >= clouds.Length)
        {
            StartCoroutine(RainAndCompleteStep());
        }
    }

    IEnumerator RainAndCompleteStep()
    {
        rain.gameObject.SetActive(true);

        yield return new WaitUntil(() => rain.animationCompleted);

        OnCloudCompleted?.Invoke();
    }
}