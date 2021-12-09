using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ParticleBurstEvent : MonoBehaviour
{
    public float timeBeforeStart = 0.1f;
    float timeCicle;
    float timeElapsed;
    StudioEventEmitter audioEvent;

    void Start()
    {
        timeElapsed = 0;
        audioEvent = GetComponent<StudioEventEmitter>();
        timeCicle = GetComponent<ParticleSystem>().emission.GetBurst(0).time;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed + timeBeforeStart >= timeCicle)
        {
            timeElapsed = 0;
            audioEvent.Play();
        }
    }
}
