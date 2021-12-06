using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Weapon : MonoBehaviour
{
    public KeyCode fireKey;
    public GameObject bullet;
    public GameObject firePoint;
    public Terraformer terraformer;
    public bool partOfAnArray = false;
    public float strength, terraformStrength, terraformRadius, fireRate;

    StudioEventEmitter soundEvent;
    float lastShoot;
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
        soundEvent = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!partOfAnArray && Input.GetKey(fireKey) && Time.time - lastShoot > 1 / fireRate)
        {
            lastShoot = Time.time;
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet, firePoint.transform.position + firePoint.transform.forward * transform.localScale.z, firePoint.transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * strength, ForceMode.Impulse);
        DestroyOnHit hit = go.GetComponent<DestroyOnHit>();
        hit.terraformer = terraformer;
        hit.strength = terraformStrength;
        hit.radius = terraformRadius;
        soundEvent.Play();
    }
}
