using UnityEngine;

public class StemGrowth : MonoBehaviour
{
    public float delay = 1.5f;

    private float totalTime = 1;
    private float timeRemaining = 0;
    private Color stemCol;

    void Start()
    {
        stemCol = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (timeRemaining < totalTime)
        {
            timeRemaining += Time.deltaTime * delay;
        }

        GetComponent<Renderer>().material.color = new Color(stemCol.r, stemCol.g, stemCol.b, timeRemaining);
    }

    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = new Color(stemCol.r, stemCol.g, stemCol.b, 0);
        SoundManager.Get().PlaySound(SoundManager.Sounds.Grass);
    }
}