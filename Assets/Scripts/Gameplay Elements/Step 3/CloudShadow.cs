using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudShadow : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] SpriteRenderer shadow = null;
    [SerializeField] SpriteRenderer sunBeam = null;
    [SerializeField] AnimationCurve alfaRegulator = null;
    void Awake()
    {
        transform.position = pivot.position;
    }

    void Update()
    {
        Vector3 from = sunBeam.transform.position;
        Vector3 to = transform.position;
        transform.up = from-to;
        float newAlfa = sunBeam.color.a;
        Debug.Log(sunBeam.color.a);
        newAlfa = alfaRegulator.Evaluate(newAlfa);
        shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, newAlfa);
        Debug.Log(shadow.color.a);
    }
}
