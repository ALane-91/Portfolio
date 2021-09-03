using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

//Allows the script to be accessed through Unity when the AudioManager is attached to an object in game.
[System.Serializable]
//No monobehaviour.
public class Sounds
{
    public string name;

    public AudioClip clip;

    //Allows volume of a sound to be altered within the game.
    [Range(0f, 1f)]
    public float volume;

    //Allows the range of a sound to be altered in the game.
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    //Ensures that the AudioSource array can't be altered from within the Unity editor, only in code.
    //Needs to be public to allow interaction with AudioManager.cs, but we do not want this to be adjustable.
    [HideInInspector]
    public AudioSource source;

}
