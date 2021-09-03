using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    //Assign the RigidBody2D property to this script from within the Unity editor.
    public Rigidbody2D rb;
    //Attach the main camera to this within the Unity editor.
    public Camera playerCam;

    Vector2 movement;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //Moves the assigned rigid body by the values taken from the movement variables, essentially enables WASD movement.
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rb.position;

        //When using Atan the Y axis is taken before X. If player character does not accurately look at mouse, add an offset after Rad2Deg, e.g: +90f, or -90f;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        //Sets the position the rigid body is currently facing to the value of angle as calculated above.
        rb.rotation = angle;
    }
}
