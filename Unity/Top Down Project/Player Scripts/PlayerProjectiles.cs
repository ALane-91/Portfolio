using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{
    //Assign a projectile impact effect to this field within the Unity editor.
    public GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Will destroy the projectile after five seconds if no object is hit.
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
