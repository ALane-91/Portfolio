using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Refers to the Sounds.cs script.
    public Sounds[] sounds;

    public static AudioManager instance;

    
    void Awake()
    {
        //Runs a check for AudioManager instances, if there is none then uses current one.
        if (instance == null)
            instance = this;
        else
        //If multiple AudioManagers are found, destroys new one.
        {
            Destroy(gameObject);
            return;
        }
        //Will end up creating two AudioManagers, but prevents sound resetting upon level/scene transition.
        DontDestroyOnLoad(gameObject);
        //Will cycle through each audio component and assign it a variable within the Sounds array.
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            //Saves the volume and pitch variable of the sound upon start of the game.
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            //Enables an option for looping by referring to the loop boolean inside of Sounds.cs.
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //Will play a sound file labelled as 'Theme' upon game start.
        Play("Theme");
    }


    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        //If sound file doesn't exist, will prevent game throwing errors in case of typos and print a message in the debug log.
        if (s == null)
        {
            Debug.LogWarning("Sound file:" + name + " not found, check naming!");
            return;
        }
        s.source.Play();


        /* Use this code inside of a collision script to play a sound upon collision. Can be adjusted for other events too.
         * In this case the event is player death, so the sound file saved in the Unity editor as 'Player Death' will play upon collision.
         * FindObjectOfType<AudioManager>().Play("PlayerDeath");
         *
         **/
    }
}
