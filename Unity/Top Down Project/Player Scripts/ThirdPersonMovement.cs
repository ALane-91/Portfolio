using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //Script for player movement and shooting.
    public CharacterController controller;
    //Attach main camera to this, not cinemachine camera.
    public Transform cam;
    public float speed = 5f;
    //When used below this ensures the player doesn't snap to face a new direction immediately.
    public float turnSmoother = 0.1f;
    //Do not make public, is used to store a value in the angle float below.
    float smoothVelocity;

    //For initial jumping.
    bool jumpCheck = false;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float distanceFromGround = 0.4f;
    float jumpForce = 1f;

    //For applying gravity to the character.
    public float gravity = -9.81f;
    Vector3 velocity;
    public float jumpHeight = 1f;

    //Shooting
    public Transform gunBarrel;
    public GameObject pistolBullet;
    public float projectileSpeed = 20f;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //The character should only move along the X and Z axis, so the value for Y is set to 0. Normalized allows for diagonal movement without increased speed.
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //For gravity effects.
        velocity.y += gravity * Time.deltaTime;
        //Speed of the fall will increase the longer the player is falling due to multiplication of Time.deltaTime.
        controller.Move(velocity * Time.deltaTime);

        //Creates a small sphere beneath the player character to verify whether they are touching the ground or not.
        jumpCheck = Physics.CheckSphere(groundCheck.position, distanceFromGround, groundMask);

        if (jumpCheck && velocity.y < 0)
        {
            //A minor amount of downward force is constantly applied to ensure the player is always grounded when not jumping.
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && jumpCheck)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        //Using deltaTime ensures the rate of movement is independent of frame rate, meaning a higher frame rate doesn't not result in higher movement speeds (See Fallout 76).
        if(direction.magnitude >= 0.1f)
        {
            //This will cause the player to look towards the direction of (movement?) the camera. Do not use Y axis, the character does not need vertical movement.
            //cam.eulerAngles.y relates to camera following.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoother);
            //Using the angle instead of targetAngle prevents snapping.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //By using Vector3.forward the movement will be in a direction, not just rotating on the spot.
            Vector3 movDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movDir.normalized * speed * Time.deltaTime);
        }

        //For Shooting a weapon!
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    

}

    
    void Shoot()
    {
        GameObject bullet = Instantiate(pistolBullet, gunBarrel.position, gunBarrel.rotation);
        Rigidbody bulletrb = bullet.GetComponent<Rigidbody>();
        bulletrb.AddForce(gunBarrel.forward * projectileSpeed, ForceMode.Impulse);
    }

    /* This was moved to a separate script for projectiles (BulletScript), script needed to be attached to the bullet prefab directly.
    //Will play a hit effect when a projectile hits a surface, as well as destroying the projectile after impact to avoid clutter/lag.
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = Instantiate(bulletImpact, transform.position, Quaternion.identity);
        Destroy(hit, 5f);
        Destroy(gameObject);
    }*/

}
