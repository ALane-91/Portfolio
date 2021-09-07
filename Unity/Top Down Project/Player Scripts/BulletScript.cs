using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody bulletrb;
    public GameObject bulletImpact;
    // Start is called before the first frame update
    void Start()
    {
        bulletrb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        GameObject hit = Instantiate(bulletImpact, transform.position, Quaternion.identity);
        Destroy(hit, 2f);
        Destroy(gameObject);
        //During testing this will show in the debug log which object was collided with.
        Debug.Log(hitInfo.name);
    }
}
