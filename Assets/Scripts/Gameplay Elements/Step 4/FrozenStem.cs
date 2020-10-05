using System;
using UnityEngine;

public class FrozenStem : MonoBehaviour
{
    Clickable clickable;

    public static event Action OnIceBroken;

    void Awake()
    {
        clickable = GetComponent<Clickable>();
    }

    void OnEnable()
    {
        GameplayController.clickableObjects.Add(clickable);
        clickable.StartCorrectState();
        GetComponent<StepObject>().Appear();
    }

    void OnMouseOver()
    {
        if (!clickable.interactable) return;

        if (Input.GetButtonDown("Left Click"))
        {
            GameplayController.clickableObjects.Remove(clickable);
            gameObject.SetActive(false);
            OnIceBroken?.Invoke();
        }
    }
}