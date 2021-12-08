using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class PlaySucesiveEvent : MonoBehaviour
{
    [Range(0, 1)]
    public float restTimeFactor = 0.3f;
    public float restTime = 3f;
    [Range(0, 1)]
    public float waitRandomFactor = 0.2f;
    public float waitTime = 0.5f;
    public StudioEventEmitter sucesiveEvent;

    bool firstPlayed = false;
    float restRandTime;
    float waitRandTime;
    float timeElapsed;
    StudioEventEmitter firstEvent;

    void Start()
    {
        timeElapsed = 0;
        restRandTime = calculateRandom(restTimeFactor, restTime);
        waitRandTime = calculateRandom(waitRandomFactor, waitTime);
        firstEvent = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstEvent.IsPlaying() || sucesiveEvent.IsPlaying()) return;

        timeElapsed += Time.deltaTime;
        if (!firstPlayed && timeElapsed + restRandTime >= restTime)
        {
            timeElapsed = 0;
            firstPlayed = true;
            waitRandTime = calculateRandom(waitRandomFactor, waitTime);
            firstEvent.Play();
        }
        else if (timeElapsed + waitRandTime >= waitTime)
        {
            timeElapsed = 0;
            firstPlayed = false;
            restRandTime = calculateRandom(restTimeFactor, restTime);
            sucesiveEvent.Play();
        }
    }

    float calculateRandom(float factor, float total)
    {
        return total - (Random.Range(-factor, factor) * total);
    }
}
