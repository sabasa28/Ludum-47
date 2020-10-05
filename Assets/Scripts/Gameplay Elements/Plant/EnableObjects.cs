using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    [SerializeField] GameObject stem = null;

    public enum EnableObject
    {
        OnEnable,
        OnDisable
    }
    public EnableObject enableObject;

    private void OnEnable()
    {
        if (enableObject == 0)
            stem.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (enableObject != 0)
        {
            stem.gameObject.SetActive(true);
        }
    }
}
