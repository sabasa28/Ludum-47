using System;
using System.Collections;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    bool onStep3 = false;

    int timesGrown = 0;
    int timesShrunk = 0;
    [SerializeField] int shrinkTarget = 5;

    [SerializeField] float growthFactor = 1.2f;
    [SerializeField] float shrinkFactor = 0.75f;

    [SerializeField] RainAnimation rain = null;
    [SerializeField] GameObject outline = null;
    [SerializeField] Transform shadow = null;

    public Draggable[] clouds;
    Clickable clickable = null;

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
        if (clickable)
        {
            if (!onStep3 || !clickable.interactable) return;

            if (Input.GetButtonDown("Left Click"))
            {
                if (timesShrunk < shrinkTarget)
                {
                    transform.localScale *= shrinkFactor;
                    ScaleShadow(shrinkFactor);
                    timesShrunk++;
                }
                else OnStepsCompleted?.Invoke();
            }
        }
    }

    void GrowCloud()
    {
        timesGrown++;
        transform.localScale *= growthFactor;
        ScaleShadow(growthFactor);

        if (timesGrown >= clouds.Length)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Draggable";
            clickable = gameObject.AddComponent<Clickable>();
            clickable.interactableDuring = Clickable.InteractableDuring.day;
            clickable.outLine = outline;
            GameplayController.clickableObjects.Add(clickable);
            clickable.EndCorrectState();

            StartCoroutine(RainAndCompleteStep2());
        }
    }

    void ScaleShadow(float scaleFactor)
    {
        Vector3 scale = new Vector3(shadow.localScale.x, shadow.localScale.y * scaleFactor, 1f);
        shadow.localScale = scale;
        // lerpear escala después
    }

    IEnumerator RainAndCompleteStep2()
    {
        rain.gameObject.SetActive(true);

        yield return new WaitUntil(() => rain.animationCompleted);

        shadow.gameObject.SetActive(true);
        shadow.GetComponent<StepObject>().Appear();
        onStep3 = true;
    }
}