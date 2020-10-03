using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemGrowth : MonoBehaviour
{
    public float delay = 10;

    private float totalTime = 1;
    private float timeRemaining = 0;
    private Color stemCol;

    void Start()
    {
        stemCol = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (timeRemaining < totalTime)
        {
            timeRemaining += Time.deltaTime * delay;
        }

        this.gameObject.GetComponent<Renderer>().material.color = new Color(stemCol.r, stemCol.g, stemCol.b, timeRemaining);
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(stemCol.r, stemCol.g, stemCol.b, 0);
    }
}
