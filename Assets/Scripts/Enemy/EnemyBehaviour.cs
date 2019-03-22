using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //Public Vars
    public float spawntimer = 5f;

    //Private Vars
    float health = 200f;

    //Script References
    Transform player;
    PlayerBehaviour playercontrolls;

    //Freeze Vars
    bool frozen = false;
    int frozencount = 0;
    float freeztime;
    float freezduration = 5f;

    //Death
    bool isdead = false;

    //Unity Vars
    Transform target;
    CapsuleCollider enmcollider;
    Rigidbody body;
    NavMeshAgent pathfinder;
    Animator anim;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playercontrolls = player.GetComponent<PlayerBehaviour>();
        pathfinder = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        enmcollider = GetComponent<CapsuleCollider>();
        spawntimer = 5f;
    }
    private void Start()
    {
        pathfinder.stoppingDistance = 3;
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        pathfinder.SetDestination(target.position);
        if (distance <= pathfinder.stoppingDistance)
        {
            Face();
        }
        else
        {
            anim.ResetTrigger("Attacking 0");
            anim.SetBool("Standing", false);
            anim.SetBool("IsWalking", true);
        }
        if (frozen && frozencount < 1)
        {
            freeztime = Time.time + freezduration;
            frozencount += 1;
        }
        if (frozen && freeztime >= Time.time && !isdead)
        {
            pathfinder.speed = 0;
            anim.speed = 0;
        }
        else if (frozen && freeztime <= Time.time && !isdead)
        {
            frozen = false;
        }
        else if (frozen == false && isdead == false)
        {
            pathfinder.speed = 3.5f;
            frozen = false;
            frozencount = 0;
            anim.speed = 1;
        }
        if (health <= 0 && isdead == false)
        {
            anim.speed = 1;
            Death();
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        //Fire Hit
        if (other.CompareTag("Fireball"))
        {
            anim.SetTrigger("Damage 0");
            Destroy(other.gameObject);
            health -= 40;
            if (frozen)
            {
                frozen = false;
            }
        }

        //Ice Hit
        if (other.CompareTag("iceShard"))
        {
            anim.SetTrigger("Damage 0");
            health -= 90;
            if (frozen == false)
            {
                frozen = true;
            }
        }
    }

    void Face()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        body.MoveRotation(Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3f));
        anim.SetTrigger("Attacking 0");
        anim.SetBool("IsWalking", false);
    }

    void Death()
    {
        ScoreManager.score += 10;
        anim.SetTrigger("Death");
        enmcollider.isTrigger = true;
        body.isKinematic = true;
        pathfinder.speed = 0;
        Destroy(gameObject, 2f);
        isdead = true;
    }
}
