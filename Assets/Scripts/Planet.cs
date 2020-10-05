using UnityEngine;

public class Planet : MonoBehaviour
{
    enum PlanetType { sun, moon }
    [SerializeField] PlanetType type = PlanetType.sun;

    [SerializeField] SunMoonController controller = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            if (type == PlanetType.sun) controller.sunShouldRotate = false;
            else controller.moonShouldRotate = false;

            if (!controller.firstPlanetCollided)
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "Below Planet";
                controller.firstPlanetCollided = true;
            }
            else controller.StartFinalAnimation();
        }
    }
}