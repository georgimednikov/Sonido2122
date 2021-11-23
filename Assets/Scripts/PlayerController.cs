using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float sensibility = 100;
    public GameObject topHalf, artilleryCannon;
    public float speed, turnSpeed, restHeight = 1.5f;
    public GameObject[] weapons;
    public Transform[] legs;
    Rigidbody rb;
    public float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        transform.Rotate(transform.up, mouseX);
        camera.transform.Rotate(Vector3.left, mouseY);

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed * Time.deltaTime);
            //transform.Rotate(transform.up, turnSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * speed * Time.deltaTime);
            //transform.Rotate(transform.up, -turnSpeed);
        }



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
            pos = camera.transform.position + camera.transform.forward * 100;
        }

        foreach (var weapon in weapons)
        {
            weapon.transform.LookAt(pos, Vector3.up);
        }
        pos.y = topHalf.transform.position.y;
        //topHalf.transform.LookAt(pos, Vector3.up);

        float posY = 0;
        foreach (var leg in legs)
        {
            posY += leg.position.y;
        }
        posY /= legs.Length;

        transform.position = new Vector3(transform.position.x, posY + restHeight, transform.position.z);
    }

    float CannonUpdate(Vector3 objPos)
    {
        float x = new Vector2(objPos.x - artilleryCannon.transform.position.x, objPos.z - artilleryCannon.transform.position.z).magnitude;
        float h = objPos.y - artilleryCannon.transform.position.y;
        float v = 5;
        float phi = Mathf.Atan2(x, h);
        float t = Mathf.Acos((((Mathf.Pow(x, 2) * -Physics.gravity.magnitude) / Mathf.Pow(v, 2)) - h) / (Mathf.Sqrt(Mathf.Pow(h, 2) + Mathf.Pow(x, 2))));
        return (t + phi) / 2;
    }
}
