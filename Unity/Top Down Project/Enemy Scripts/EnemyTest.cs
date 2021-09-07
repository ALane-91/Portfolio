using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    public int health = 100;

    public GameObject death;

    public void DamageTaken(int damage)
    {
        health -= damage;
        if (health <=50)
        {
            Debug.Log("Target has at least half health");
        }
        else if (health <=25)
        {
            Debug.Log("Target is almost dead");
        }
        if (health <= 0)
        {
            Debug.Log("Target should be dead");
            Die();
        }
    }

    void Die()
    {
        Instantiate(death, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
