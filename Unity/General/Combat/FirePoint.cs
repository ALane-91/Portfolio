using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    //Attach to a the object where you want the bullet to come from.
    public Transform firePoint;
    public GameObject bullet;

    /*
    //Will trigger on every mouse press, not just left click
    private void OnMouseDown()
    {
        Shoot(); 
    }
    */

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
        //Creates a copy of the GameObject bullet at the current position of the gun and at the current rotation.
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
