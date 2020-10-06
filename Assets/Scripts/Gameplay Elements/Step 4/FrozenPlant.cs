using System;
using UnityEngine;

public class FrozenPlant : MonoBehaviour
{
    [SerializeField] int iceBreakTarget = 0;
    int timesIceBroken = 0;

    public static Action OnIceBroken;

    void OnEnable()
    {
        FrozenStem.OnIceBroken += IncreaseBrokenIce;
    }

    void OnDisable()
    {
        FrozenStem.OnIceBroken -= IncreaseBrokenIce;
    }

    void IncreaseBrokenIce()
    {
        timesIceBroken++;

        if (timesIceBroken >= iceBreakTarget) OnIceBroken?.Invoke();
    }
}