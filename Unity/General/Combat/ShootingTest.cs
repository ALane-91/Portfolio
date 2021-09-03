using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    //Script written as part of a group project to test a projectile raycast in a VR setting.
    public float targetHealth = 50f;

    //This method will change the test objects colour based on health value. When target health reaches 0 the target is destroyed.
    public void TakeDamage(float amount)
    {
        targetHealth -= amount;
        if (targetHealth <= 0f)
        {
            //Calls the die method at the bottom of this script.
            Die();
        }
        if (targetHealth < 40f)
        {
            //Will change the targets colour to yellow if health goes below 40.
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (targetHealth < 20f)
        {
            //If the targets health goes below 20 they will turn red.
            GetComponent<Renderer>().material.color = Color.red;
        }

    }

    void Die()
    {
        print("Target dead");
        Destroy(gameObject);
    }
}
