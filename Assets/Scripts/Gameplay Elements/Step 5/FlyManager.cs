using UnityEngine;

public class FlyManager : MonoBehaviourSingleton<FlyManager>
{
    public int fliesLeft = 0;
    public GameObject finalCollider;
    public GameObject child;

    public void OnFlyDead()
    {
        fliesLeft--;
        if (fliesLeft <= 0)
        {
            finalCollider.SetActive(true);
            child.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}