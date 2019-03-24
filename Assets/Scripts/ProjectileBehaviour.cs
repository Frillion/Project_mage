using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float mvspeed = 20f;
    public float LifeSpan = 12f;
    float TimeAlive;
    Rigidbody projbody;
    
    private void Awake()
    {
        projbody = GetComponent<Rigidbody>();
        Destroy(projbody.gameObject, LifeSpan);
    }
    private void FixedUpdate()
    {
        projbody.MovePosition(transform.position + transform.forward * mvspeed * Time.deltaTime);
    }
}
