using UnityEngine;

public class RainAnimation : MonoBehaviour
{
    public bool animationCompleted = false;

    public void SetInactive()
    {
        animationCompleted = true;

        gameObject.SetActive(false);
    }
}