using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    //This script is good for beam weapons/lasers.
    public Transform firePoint;
    //Can be used to manually set the damage values, both from here and in the editor.
    public int damage = 40;
    public GameObject hitEffect;
    public LineRenderer beamRenderer;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("Fire1"))
        {
            //Requires a shoot script to be written, see below.
            StartCoroutine (Shoot());
        }

       IEnumerator Shoot()
        {
            //Starts the Raycast from the weapons current position
            RaycastHit2D targetInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

            if (targetInfo)
            {
                EnemyScript enemy = targetInfo.transform.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    //If you want to use a fixed damage value.
                    enemy.DamageTaken(20);
                    //If you want to use the value stored in the damage variable.
                    enemy.DamageTaken(damage);
                }

                Instantiate(hitEffect, targetInfo.point, Quaternion.identity);

                beamRenderer.SetPosition(0, firePoint.position);
                beamRenderer.SetPosition(1, targetInfo.point);

                //This is a way of testing what target you're hitting and ensuring correct tags.
                Debug.Log(targetInfo.transform.name);

            }
            else
            {
                //If the player misses a target, the beam effect will look like it carries off into the distance.
                beamRenderer.SetPosition(0, firePoint.position);
                beamRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
            }

            beamRenderer.enabled = true;

            //Wait for one frame, can also make this smaller, say 0.02.
            yield return 0;

            beamRenderer.enabled = false;
        }
    }
}
