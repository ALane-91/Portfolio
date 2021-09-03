using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    //Can set a flat value for damage here.
    public int damage = 20;
    //This will allow a sprite effect to play when projectile impacts with target when attached to the script in Unity.
    public GameObject impactEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D targetInfo)
    {
        //Will print out the name of the target hit (Enemy, friendly, wall, etc)
        EnemyScript enemy = targetInfo.GetComponent<EnemyScript>();

        if (enemy != null)
        {
            enemy.DamageTaken(40);
        }

        //Will use a particle effect attached to the impactEffect variable whenever a bullet collides with an enemy/object.
        Instantiate(impactEffect, transform.position, transform.rotation);
        Debug.Log(targetInfo.name);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
