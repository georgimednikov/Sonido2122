using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    Vector3 lastpos;

    void Start()
    {
        lastpos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastpos, Input.mousePosition) > Mathf.Epsilon)
        {
            Vector3 dir = Input.mousePosition - lastpos;
            Debug.Log(dir);
            lastpos = Input.mousePosition;

            if (Input.GetButton("Fire2"))
            {
                dir /= 10f;
                transform.Rotate(-transform.forward, dir.x);
            }
            else if(Input.GetButton("Fire1"))
            {
                dir /= 100f;
                transform.Translate(new Vector3(dir.x, 0, dir.y), Space.World);
            }
        }
    }
}
