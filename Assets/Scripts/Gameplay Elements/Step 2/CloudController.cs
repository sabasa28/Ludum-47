using System;
using System.Collections;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    bool onStep3 = false;
    bool onCorrectState = false;

    int timesGrown = 0;
    int timesShrunk = 0;
    [SerializeField] int shrinkTarget = 5;

    [SerializeField] float growthFactor = 1.2f;
    [SerializeField] float shrinkFactor = 0.75f;

    [SerializeField] RainAnimation rain = null;
    [SerializeField] GameObject outline = null;

    public Draggable[] clouds;

    static public Action OnStepsCompleted;

    void Awake()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].Reaction += GrowCloud;
        }
    }

    private void OnMouseOver()
    {
        if (!onStep3 && !onCorrectState) return;

        if (Input.GetButtonDown("Left Click"))
        {
            if (timesShrunk < shrinkTarget)
            {
                transform.localScale *= shrinkFactor;
                timesShrunk++;
            }
            else OnStepsCompleted?.Invoke();
        }
    }

    void GrowCloud()
    {
        timesGrown++;
        transform.localScale *= growthFactor;

        if (timesGrown >= clouds.Length)
        {
            StartCoroutine(RainAndCompleteStep2());
        }
    }

    IEnumerator RainAndCompleteStep2()
    {
        rain.gameObject.SetActive(true);

        yield return new WaitUntil(() => rain.animationCompleted);

        GetComponent<SpriteRenderer>().sortingLayerName = "Draggable";
        Clickable clickable = gameObject.AddComponent<Clickable>();
        clickable.interactableDuring = Clickable.InteractableDuring.night;
        clickable.outLine = outline;
        clickable.OnCorrectStateStart += () => onCorrectState = true;
        clickable.OnCorrectStateEnd += () => onCorrectState = false;
        GameplayController.clickableObjects.Add(clickable);

        onStep3 = true;
    }
}