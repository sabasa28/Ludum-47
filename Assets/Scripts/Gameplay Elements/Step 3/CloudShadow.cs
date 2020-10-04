using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudShadow : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] SpriteRenderer shadow = null;
    [SerializeField] SpriteRenderer sunBeam = null;

    void Awake()
    {
        transform.position = pivot.position;
    }

    void Update()
    {
        Vector3 from = sunBeam.transform.position;
        Vector3 to = transform.position;
        transform.up = from-to;
        shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, sunBeam.color.a);
    }
}
