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
        floormask = LayerMask.GetMask("Ground");
        anim = GetComponent<Animator>();
        playerbody = GetComponent<Rigidbody>();
        Projectileoffset = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        mvX = Input.GetAxis("Horizontal");
        mvZ = Input.GetAxis("Vertical");
        mv = new Vector3(mvX,0.0f,mvZ);
        AnimateMove(mvX,mvZ);
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

    void ShootFire()
    {
        Quaternion basespawn = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        LastFire = Time.time + FireCooldown;
        anim.SetBool("attack_short_001", true);
        Object.Instantiate(Fire, transform.position + transform.forward + Projectileoffset, basespawn, transform);
    }

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
}
