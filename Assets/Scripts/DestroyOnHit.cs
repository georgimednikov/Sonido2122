using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public Terraformer terraformer;
    public float strength = 10, radius = 3;
    public GameObject impactParticles;

    protected void Start()
    {
        Destroy(gameObject, 10f);
    }
    protected void OnCollisionEnter(Collision collision)
    {
        terraformer?.terraform(collision.GetContact(0).point, radius, strength);
        if (impactParticles != null) Instantiate(impactParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
