using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundAttenuation : MonoBehaviour
{
    public StudioEventEmitter[] emitters;

    // Update is called once per frame
    void Update()
    {
        foreach (var emitter in emitters)
        {
            Vector3 dir = emitter.gameObject.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir.normalized, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    Debug.DrawRay(transform.position, dir.normalized * hit.distance, Color.green);
                    emitter.SetParameter("Wall", 0);
                }
                else
                {
                    Debug.DrawRay(transform.position, dir.normalized * hit.distance, Color.red);
                    emitter.SetParameter("Wall", 1);
                }
            }
        }
    }
}
