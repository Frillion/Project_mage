using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //PUBLIC VARS
    //THE OBJECT TO BE SPAWNED
    public GameObject spawn;
    //SPAWNING VARS
    public Transform[] spawnpoints;

    //PRIVATE VARS
    //VARS TO HELP REFERENCE TO THE ENEMY SCRIPT
    EnemyBehaviour enemycontroller;

    //VARS TO HELP REFERENCE TO THE PLAYER SCRIPT
    Transform player;
    PlayerBehaviour playercontrolls;

    void Awake() 
    {
        enemycontroller = spawn.GetComponent<EnemyBehaviour>();

        //PLAYER REFERENCES
        player = GameObject.FindWithTag("Player").transform;
        playercontrolls = player.GetComponent<PlayerBehaviour>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", enemycontroller.spawntimer, enemycontroller.spawntimer);
    }


    void Spawn()
    {
        if (playercontrolls.isdead || playercontrolls.ispaused)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, (spawnpoints.Length - 1));
        Object.Instantiate(spawn, spawnpoints[spawnPointIndex].position, spawnpoints[spawnPointIndex].rotation);
    }
}
