using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    public Transform target;
    public float smooth = 5f;

    GameObject[] Walls;
    Transform Wall;
    LayerMask wallmask;
    Vector3 offset;
    MeshRenderer WallRender;
    int WallsLength;

    private void Awake()
    {
        wallmask = LayerMask.GetMask("Wall");
    }
    private void Update()
    {
        WallsLength = Walls.Length;
    }
    private void Start()
    {
        offset = transform.position - target.position;
        Walls = GameObject.FindGameObjectsWithTag("Wall");
    }
    private void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smooth * Time.deltaTime);
        Ubstruct();
    }

    void Ubstruct()
    {
        RaycastHit hit;

        

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, wallmask))
        {
            Wall = hit.transform;
            Wall.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
        else
        {
            for (int i = 0; i < WallsLength; i++)
            {
                Walls[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }
}
