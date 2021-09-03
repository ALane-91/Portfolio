using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    //Can use the control editor within the Unity engine to adjust any controls, the "Jump", "Horizontal" etc functions can be assigned new controls there.
    //References the CharacterController2D script created by Brackeys, can be found here : https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs.
    public CharacterController2D controller;

    //Variables relating to the SaveData and SaveSystem files;
    public int level = 0;
    public int health = 100;

    //Dictates player movement speed.
    public float runSpeed = 40f;

    //Horizontal move variable.
    float hMove = 0f;

    bool jump = false;

    bool crouch = false;

    //Attach this script to a Save/Load button and assign the correct function in the Unity editor, so for save button attach SavePlayer()
    //This will interact with the SaveSystem.cs file allowing the player to save their progress.
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    //Will load saved data using the SaveSystem.cs so a player can continue their game.
    public void LoadPlayer()
    {
        SaveData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }


    // Update is called once per frame
    void Update()
    {
        //This will print a message to the debug log showing current direction, -1 is left, 0 is nothing, 1 is right.
        Debug.Log (Input.GetAxisRaw("Horizontal"));
        //Works with multiple control methods, controller, keyboard, touch screen, very basic
        //Input.GetAxisRaw("Horizontal");

        //Better method of doing it using a variable
        //Multiplies horizontal movement speed by the value of the runSpeed variable (By default 40).
        hMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            //Changes the value of the jump boolean to true if jump button is pressed.
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        //Method for moving the character.
        //First false is for crouching, second false is for jumping.
        controller.Move(hMove * Time.fixedDeltaTime , false, jump);
        //Ensures jump returns to false once the button is released.
        jump = false;
        //controller.Move(hMove);
    }
}
