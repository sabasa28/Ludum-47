using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] Clickable[] clickableObjects = null;
    private void Awake()
    {
        SunMoonController smController = FindObjectOfType<SunMoonController>();
        smController.OnStateChange = OnDayStart;
        clickableObjects = FindObjectsOfType<Clickable>();
    }

    public void OnDayStart(bool day)
    {
        for (int i = 0; i < clickableObjects.Length; i++)
        {
            if (clickableObjects[i].gameObject.activeInHierarchy)
            {
                if ((clickableObjects[i].interactableDuring == Clickable.InteractableDuring.day) == day)
                    clickableObjects[i].OnStartCorrectState();
                else
                    clickableObjects[i].OnEndCorrectState();
            }
        }
    }
}
