using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArray : MonoBehaviour
{
    public KeyCode fireKey;
    public Weapon[] weapons;
    public float fireRate;
    int i = 0;
    

    float lastShoot;
    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
        foreach (var item in weapons)
        {
            item.partOfAnArray = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(fireKey) && Time.time - lastShoot > 1 / fireRate)
        {
            lastShoot = Time.time;
            weapons[i].Shoot();
            i = (i + 1) % weapons.Length;
        }
    }
}
