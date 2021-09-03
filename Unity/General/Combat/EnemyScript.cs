using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Allows for the particle system to be assigned within Unity editor, attached particle system will play on effect.
    [SerializeField] private GameObject _particleTest;

    //Sets a health value for the enemy
    public int hp = 100;
    //Can be used to call a death effect animation upon trigger.
    public GameObject deathEffect;

    //Has to be public so it can be accessed from other files.
    public void DamageTaken(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            //Only needed if using a death animation, for simplicity can use the code below.
            Death();
            //This code will just destroy the object when health hits 0.
            Destroy(gameObject);
        }
    }

    void Death()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Can also be written this way to look cleaner.
        bool playerHit = collision.collider.GetComponent<PlayerScript>() != null;
        if (playerHit)
        {
            Destroy(gameObject);
        }

        //Another method of writing the same code, adjust player to whatever the character is called (Not sure if correct).
        PlayerScript player = collision.collider.GetComponent<PlayerScript>();
        if (player != null)
        {
            Destroy(gameObject);
            Instantiate(_particleTest, transform.position, Quaternion.identity);
            return;
        }

        //If an enemy collides with another enemy, does nothing.
        EnemyScript enemy = collision.collider.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            return;
        }

        //Creates a collision array, but only with one slot. Will destroy if hit by an object.
        if (collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(_particleTest, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        
        
        /*
        //Will check if the player control script is attached to object colliding, if so enemy will be deleted.
        if (collision.collider.GetComponent<PlayerScript>() != null)
        {
            Destroy(gameObject);
        }*/
    }

}
