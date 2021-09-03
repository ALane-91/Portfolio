using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform[] Points;

    /*Create platform paths in Unity by creating a game object and assigning as many waypoints as desired as children to this game object.
    Create game object, rename to platform pathing, or something memorable.
    Create a second game object rename to "Platform". Use this to create a prefab to avoid recreating the platform each use. 
    Set desired platform block as a child of this. Position child so parent is centralised. Attach Script to parent, not the child.*/

    public IEnumerator<Transform> GetPathEnumerator()
    {
        //requires at least one point after the starting position to make a path, if there is none the script won't run.
        if (Points == null || Points.Length < 1)
            yield break;

        var direction = 1;
        var index = 0;
        
        while (true)
        {
            yield return Points[index];

            if (index <= 0)
                direction = 1;
            else if (index >= Points.Length - 1)
                direction = -1;

            index = index + direction;
        }

    }
    //Built in Unity tool for handling objects.
    public void OnDrawGizmos()
    {
        //Required as the Points array isn't instantiated without a value, meaning null is required.
        if (Points == null || Points.Length < 2)
            return;

        for (var i = 1; i < Points.Length; i++)
        {
            Gizmos.DrawLine(Points[i - 1].position, Points[i].position);
        }
    }
}

