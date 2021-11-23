using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Camera camera;
    public GameObject followTarget;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null) camera = Camera.main;
        lastPos = followTarget.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = followTarget.transform.position - lastPos;
        camera.transform.position = camera.transform.position + delta;
        lastPos = followTarget.transform.position;
    }
}
