using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioDimensionCalculator : MonoBehaviour
{
    public Transform objective;
    public float firstRadius;
    public float secondRadius;

    StudioEventEmitter audioEvent;

    private void Start()
    {
        audioEvent = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        float value;
        float dist = Vector3.Distance(transform.position, objective.position);
        if (dist <= firstRadius) value = 0;
        else if (dist >= secondRadius) value = 1;
        else value = (dist - firstRadius) / (secondRadius - firstRadius);
        audioEvent.SetParameter("3D Audio", value);
    }
}
