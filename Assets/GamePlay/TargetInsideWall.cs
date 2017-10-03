using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInsideWall : MonoBehaviour
{

    public bool insideWall;

    // Use this for initialization
    void Start()
    {
        insideWall = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            insideWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            insideWall = false;
        }
    }
}
