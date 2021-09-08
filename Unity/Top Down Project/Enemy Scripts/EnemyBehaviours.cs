using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyBehaviours : MonoBehaviour
{
    //GameObject enemy;
    public float speed = 8f;
    public float sightRange = 5f;
    Transform target;
    NavMeshAgent actor;

    // Start is called before the first frame update
    void Start()
    {
        //If .transform isn't added this line will not work as the PlayerCharacter is a GameObject.
        target = PlayerManager.instance.PlayerCharacter.transform;
        actor = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(target.position, transform.position);

        if (playerDistance <= sightRange)
        {
            //If a Navmesh Error occurs go to Window>AI>Navigation and then click bake in the bake tab.
            actor.SetDestination(target.position);
            //Warps the enemy to the player when within range of the circle, useful for teleporting enemies.
            //actor.Warp(target.position);

        if(playerDistance <= actor.stoppingDistance)
            {
                //An attack command can be added here.
                //Causes the enemy to face the target.
                LookAtTarget();
            }
        }
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //No Y axis value is needed as the enemies won't be looking up or down in context of this game.
        Quaternion actorRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //Increase the value of the number after Time.deltaTime to alter rotation speed.
        transform.rotation = Quaternion.Slerp(transform.rotation, actorRotation, Time.deltaTime * 2f);
        //If below line is used, the enemy will snap to face the player instantly.
        //transform.rotation = actorRotation;
    }

    /*
    //Used to see a visual representation of how far an enemy can see.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    */

    void Flee(bool flee)
    {
        if (flee == false)
        {

        }
        else if (flee == true)
        {

        }
    }
}
