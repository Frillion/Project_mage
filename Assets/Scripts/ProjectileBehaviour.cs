using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float mvspeed = 20f;
    Rigidbody projbody;
    private void Awake()
    {
        projbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        projbody.MovePosition(transform.position + transform.forward * mvspeed * Time.deltaTime);
    }
}
