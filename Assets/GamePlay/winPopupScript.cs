using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winPopupScript : MonoBehaviour
{

    private int timer;

    // Use this for initialization
    void Start()
    {
        timer = 90;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(0, transform.position.y + Mathf.Sin(Mathf.Deg2Rad * timer) * 0.1f, 0);
        if (timer > 0)
        {
            timer--;
        }
    }
}