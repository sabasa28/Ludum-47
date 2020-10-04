using UnityEngine;

public class Cloud : MonoBehaviour
{
    Draggable draggable;

    void Awake()
    {
        draggable = GetComponent<Draggable>();
    }

    void Start()
    {
        draggable.Reaction += () => gameObject.SetActive(false);
    }
}