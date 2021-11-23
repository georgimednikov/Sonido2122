using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public KeyCode fireKey;
    public GameObject bullet;
    public GameObject firePoint;
    public Terraformer terraformer;
    public bool partOfAnArray = false;
    public float strength, terraformStrength, terraformRadius, fireRate;
    float lastShoot;
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
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
        //firePoint.GetComponent<AudioSource>()?.Play();
        go.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * strength, ForceMode.Impulse);
        DestroyOnHit hit = go.GetComponent<DestroyOnHit>();
        hit.terraformer = terraformer;
        hit.strength = terraformStrength;
        hit.radius = terraformRadius;
    }
}
