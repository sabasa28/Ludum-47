using UnityEngine;

public class Dirt : MonoBehaviour
{
    public GameObject stem1;

    private void OnMouseDown()
    {
        stem1.SetActive(true);

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        SoundManager.Get().PlaySound(SoundManager.Sounds.SeedPlant);
    }
}