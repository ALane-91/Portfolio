using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Create an empty child object for the player character and name it firePoint, align the object with the end of players weapon.
    public Transform firePoint;
    //Assign the bullet prefab to this field within the Unity Editor.
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //By creating the bullet as a gameObject it can be altered in flight.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //Shoots a projectile and applies speed based on the bulletForce variable at the top of this script, adjust as necessary. Essentially player movement speed, but for a bullet.
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
