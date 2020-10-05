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
    [SerializeField] float shadowGrowthFactor = 1.02f;
    [SerializeField] float shadowShrinkFactor = 0.975f;


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
                    ScaleShadow(shadowShrinkFactor);
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
        ScaleShadow(shadowGrowthFactor);

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
        shadow.GetComponent<SpriteRenderer>().size = new Vector2 (shadow.GetComponent<SpriteRenderer>().size.x, shadow.GetComponent<SpriteRenderer>().size.y * scaleFactor);
        // lerpear escala después
    }

    IEnumerator RainAndCompleteStep2()
    {
        rain.gameObject.SetActive(true);

        yield return new WaitUntil(() => rain.animationCompleted);

        shadow.gameObject.SetActive(true);
        onStep3 = true;
    }
}