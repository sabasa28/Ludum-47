using UnityEngine;

public class Clickable : MonoBehaviour
{
    public enum InteractableDuring
    {
        day,
        night
    }
    public InteractableDuring interactableDuring;

    public bool startActive = true;
    protected bool interactable = false;

    [SerializeField] GameObject outLine = null;
    SpriteRenderer sr = null;
    Color grayTone = new Color (0.4f,0.4f,0.4f,1.0f);

    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!startActive) gameObject.SetActive(false);
    }

    protected void OnMouseOver()
    {
        if (interactable) outLine.SetActive(true);
        else outLine.SetActive(false);
    }

    protected void OnMouseExit()
    {
        if (interactable) outLine.SetActive(false);
    }

    public void OnStartCorrectState()
    {
        sr.color = Color.white;
        interactable = true;
    }

    public void OnEndCorrectState()
    {
        outLine.SetActive(false);
        sr.color = grayTone; //no funcionaria con los que tengan cambiados los tonos desde antes en vez de estar en blanco
        interactable = false;
    }
}