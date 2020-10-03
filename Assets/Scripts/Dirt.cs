using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public GameObject stem1;

    private void OnMouseDown()
    {
        Debug.Log("quiero nacer!");

        stem1.SetActive(true);

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
