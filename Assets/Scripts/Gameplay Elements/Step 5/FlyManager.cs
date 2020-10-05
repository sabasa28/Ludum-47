using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyManager : MonoBehaviourSingleton<FlyManager>
{
    public int fliesLeft = 0;

    public void OnFlyDead()
    {
        fliesLeft--;
        if (fliesLeft <= 0)
            Debug.Log("Condicion de que murieron todas las flies");
    }
}
