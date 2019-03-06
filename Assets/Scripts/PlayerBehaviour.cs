using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Public Vars
    public float movementspeed = 6f;
    public Object Fire;
    public Object ice;

    //Private Vars
    //health
    float health = 200f;

    //Movement
    float mvX;
    float mvZ;

    //Rotation
    int floormask;
    float raylength = 100f;

    //Fire Vars
    float FireCooldown = 1.0f;
    float LastFire;

    //Ice  Vars
    float IceCooldown = 10.0f;
    float LastIce;

    //Unity Based Vars
    Vector3 Projectileoffset;
    Vector3 mv;
    Rigidbody playerbody;
    Animator anim;

    private void Awake()
    {
        //Get All Components needed for the code to function
        floormask = LayerMask.GetMask("Ground");
        anim = GetComponent<Animator>();
        playerbody = GetComponent<Rigidbody>();
        //To get the projectiles to spawn in the right location compared to the player model
        Projectileoffset = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        //All variables for movement
        mvX = Input.GetAxis("Horizontal");
        mvZ = Input.GetAxis("Vertical");
        mv = new Vector3(mvX,0.0f,mvZ);

        //The function for animating movement
        AnimateMove(mvX,mvZ);

        //Attacks
        if (Input.GetButton("Fire1") && Time.time >= LastFire)
        {
            ShootFire();
        }
        else if (Input.GetButton("Fire2") && Time.time >= LastIce)
        {
            ShootIce();
        }
        else
        {
            anim.SetBool("attack_short_001", false);
        }
    }


    private void FixedUpdate()
    {
        Turning();
        playerbody.MovePosition(transform.position + mv * movementspeed * Time.deltaTime);
    }


    //If Player Right Clicks(Fire2) then this shoud run 
    //It Spawns a fan of iceciles
    void ShootIce()
    {
        Quaternion basespawn = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        LastIce = Time.time + IceCooldown;
        anim.SetBool("attack_short_001", true);
        for (int i = 0; i < 2; i++)
        { 
            Object.Instantiate(ice, transform.position + transform.forward + Projectileoffset, basespawn, transform);
            basespawn *= Quaternion.Euler(0, 15, 0);
        }
        Object.Instantiate(ice, transform.position + transform.forward + Projectileoffset, basespawn, transform);
        basespawn *= Quaternion.Euler(0, 295, 0);
        Object.Instantiate(ice, transform.position + transform.forward + Projectileoffset, basespawn, transform);
        basespawn *= Quaternion.Euler(0, 15, 0);
        Object.Instantiate(ice, transform.position + transform.forward + Projectileoffset, basespawn, transform);
    }


    //If The Player Left Clicks(Fire1) then  this should run
    //It Spawns a fireball
    void ShootFire()
    {
        Quaternion basespawn = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        LastFire = Time.time + FireCooldown;
        anim.SetBool("attack_short_001", true);
        Object.Instantiate(Fire, transform.position + transform.forward + Projectileoffset, basespawn, transform);
    }

    //Turning the player tward the mouse
    void Turning()
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camray, out floorHit, raylength, floormask))
        {
            Vector3 playtomouse = floorHit.point - transform.position;
            playtomouse.y = 0.0f;

            Quaternion rot = Quaternion.LookRotation(playtomouse);
            playerbody.MoveRotation(rot);
        }
    }

    //Animations for Movement
    void AnimateMove(float mvX,float mvZ)
    {
        if (mvX != 0 || mvZ != 0)
        {
            anim.SetBool("idle_combat", false);
            anim.SetBool("move_forward", true);
        }
        else
        {
            anim.SetBool("move_forward", false);
            anim.SetBool("idle_combat", true);
        }
    }

    //If The player Is Hit
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Sword")) 
        {
            anim.SetTrigger("Damage_Wisard");
        }
    }
}
