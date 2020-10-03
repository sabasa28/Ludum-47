using UnityEngine;

public class Draggable : Clickable
{
    bool dragged = false;
    bool colliding = false;

    Vector3 dragStartPosition;
    Vector3 worldMousePosition;
    Vector3 mouseOffset;


    public Collider2D interactionCollider;

    void Awake()
    {
        base.Awake();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == interactionCollider)
            colliding = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == interactionCollider)
            colliding = false;
    }

    void OnMouseOver()
    {
        base.OnMouseOver();

        if (!interactable)
        {
            dragged = false;
            return;
        }

        if (Input.GetButtonDown("Left Click"))
        {
            dragged = true;
            dragStartPosition = transform.position;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -(Camera.main.transform.position.z);
            worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            mouseOffset = transform.position - worldMousePosition;
        }
        if (Input.GetButtonUp("Left Click"))
            dragged = false;
    }

    void OnMouseDrag()
    {
        if (!interactable)
        {
            if (transform.position != dragStartPosition && dragged)
                transform.position = dragStartPosition;

            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -(Camera.main.transform.position.z);
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = worldMousePosition + mouseOffset;
    }

    void OnMouseExit()
    {
        if (!dragged) base.OnMouseExit();
    }

    void Update()
    {
        if (Input.GetButtonUp("Left Click") && colliding)
            Debug.Log("interaction");
    }
}