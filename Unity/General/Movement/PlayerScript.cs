using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    //This file contains code for movement in a few types of games, using mouse drag and WASD movement. 
    //Declares the RigidBody variable.
    Rigidbody2D rigBod;
    //Declares the value of the speed variable.
    public float speed = 5.0f;
    //Bool to check if player jump status is true or false.
    private bool _jump;
    //Bool to check if the player character was launched using mouse up event.
    private bool _playerWasLaunched;
    //Bool to check how long a player is in the air.
    private float _airTime;
    //A variable that will be used to declare the starting position.
    Vector3 _initialPosition;

    //Allows for manual adjustment of the multiplier from within Unity.
    [SerializeField] private float _speedMultiplier = 500;



    // Start is called before the first frame update
    void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        //Allows the character model to rotate, or in this case not rotate when affected by gravity etc.
        rigBod.freezeRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the first set of array coords to be where the player currently is, so can draw a line from players current position to starting position.
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        //Sets the second set of array coords to be linked to the players starting location.
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        if (_jump && rigBod.velocity.magnitude <=0.1)
        {
            _airTime += Time.deltaTime;
        }

        if (_playerWasLaunched && rigBod.velocity.magnitude <= 0.1)
        {
            _airTime += Time.deltaTime;
        }

        //If/Or startement that triggers if characters position reaches set position on Y or X axis, level will then restart.
        //Will also trigger a restart if the character is airborne for more than 3 seconds as this is unintended currently.
        //Can change this to be any condition.
        if (transform.position.y > 10 || transform.position.y < -10 || transform.position.x >10 || transform.position.x < -10 || _airTime > 3)
        {
            //Declares a variable that takes name of current level and stores as a string.
            string currentSceneName = SceneManager.GetActiveScene().name;
            //Reloads current level if parameters are met.
            SceneManager.LoadScene(currentSceneName);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if(Input.GetKeyDown(KeyCode.W))
        {
            rigBod.velocity = transform.up * speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rigBod.velocity = -transform.right * speed;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rigBod.velocity = transform.right * speed;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rigBod.velocity = -transform.up * speed;
        }
    }

    //When game is started, will do this.
    private void Awake()
    {
        _initialPosition = transform.position;
    }



    //Will do something when the mouse button is clicked
    private void OnMouseDown()
    {
        //Add a shoot script here.

        //Toggles line renderer on when mouse is pressed.
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        //Uses the _initialPosition variable and calculates distance between there and current position.
        Vector2 directionToInitialPosition = _initialPosition - transform.position;

        //Will fling the character away from the mouse when released and increase speed.
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * 5);
        //Will set gravity to 1 when mouse is released so character can fall.
        GetComponent<Rigidbody2D>().gravityScale = 1;

        //Cleaner way of writing above code,  but using this method the multiplier will need to be huge (500+) and has to be set here.
        //rigBod.AddForce(directionToInitialPosition * 5);

        //Cleaner way of writing code, the _speedMultiplier variable can be altered at any time in Unity.
        rigBod.AddForce(directionToInitialPosition * _speedMultiplier);

        //Calls the rigBod variable as opposed to the RigidBody in the editor itself.
        rigBod.gravityScale = 1;

        //Temporary jump function bool test.
        _jump = true;

        //Toggles line renderer off when mouse is released.
        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        //New Vector to move character to mouse position.
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ensures that the character doesn't move to the Z position of the camera.
        transform.position = new Vector3(newPosition.x, newPosition.y);


        //Test this to adjust scale when dragged, rotate to face away from drag, then stretch outwards.
        //transform.localScale = +0.03;
    }
}
