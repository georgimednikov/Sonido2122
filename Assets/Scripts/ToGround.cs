using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ToGround : MonoBehaviour
{
    public GameObject inverseTarget;
    public ToGround otherLeg;
    public float stepHeight = 2, stepDistance = 2;
    public StudioEventEmitter stomp;
    public StudioEventEmitter legMoving;

    Vector3 initialPos;
    float t = 0;

    [HideInInspector]
    public bool moving;

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + transform.up * 0.25f, transform.TransformDirection(-Vector3.up), out hit, stepHeight))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            transform.position = hit.point;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * stepHeight, Color.white);

        }

        if (Vector3.Distance(inverseTarget.transform.position, transform.position) >= stepDistance && !moving && !otherLeg.moving)
        {
            moving = true;
            initialPos = inverseTarget.transform.position;
        }

        if (moving)
        {
            if (!legMoving.IsPlaying())
                legMoving.Play();

            if (t <= 1) inverseTarget.transform.position = Vector3.Lerp(initialPos, (initialPos + transform.position) / 2 + transform.up * 0.2f, t);
            else inverseTarget.transform.position = Vector3.Lerp((initialPos + transform.position) / 2 + transform.up * 0.2f, transform.position, t - 1);
            t += 0.05f;
            if ((t >= 2))
            {
                stomp.Play();
                moving = false;
                t = 0;
            }
        }

    }
}
