using UnityEngine.Audio;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    //This script is a test for attaching to a character directly, other sound script is for use with AudioManager.
    //A demonstration script on how to get a sound playing when an action is performed, in this case a jump.
    public Vector2 JumpPower;

    // Update is called once per frame
    void Update()
    {
        //Will play a sound effect on left mouse click.
        if (Input.GetButtonDown("Fire1"))
        {
            //Add an AudioSource component to the player character inside the Unity editor, this will call that.
            //If you don't hear sound when testing play in Unity, check the mute audio button.
            GetComponent<AudioSource>().Play();
            //GetComponent<RigBod>().AddForce(JumpPower);
        }

        if (Input.GetButtonDown("Shoot"))
        {

            GetComponent<AudioSource>().Play();
        }
        
    }
}
