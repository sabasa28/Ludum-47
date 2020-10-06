using System;

public class FlyManager : MonoBehaviourSingleton<FlyManager>
{
    public int fliesLeft = 0;

    public static Action OnFliesShooed;

    public void OnFlyDead()
    {
        fliesLeft--;
        if (fliesLeft <= 0)
        {
            gameObject.SetActive(false);
            OnFliesShooed?.Invoke();
        }
    }
}