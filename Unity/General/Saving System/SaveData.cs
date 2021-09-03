using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int level;
    public int health;
    //Creates an array that will store players current position data when saved.
    public float[] position;

    public SaveData(CharacterController player)
    {
        //Requires a corresponding variable within the CharacterController class, so a health and level variable in this case.
        level = player.level;
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }
}
