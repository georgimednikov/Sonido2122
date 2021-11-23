using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : DestroyOnHit
{
    Camera camera;
    public float angleDeviation;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        camera = Camera.main;
        transform.Rotate(new Vector3(Random.Range(-angleDeviation, angleDeviation), Random.Range(-angleDeviation, angleDeviation), 0));
        RaycastHit hit;
        Vector3 pos;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);//Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 50);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            pos = hit.point;

        }
        else
        {
            pos = camera.transform.position + camera.transform.forward * 50;
        }
        GameObject go = new GameObject();
        go.transform.position = pos;
        go.AddComponent<DestroyOnHit>();
        GetComponent<Polarith.AI.Move.AIMSeek>().GameObjects.Add(go);
    }
}
