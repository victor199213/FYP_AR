using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winPopupScript : MonoBehaviour
{

    private int timer;
    public int returnMenuTimer;

    // Use this for initialization
    void Start()
    {
        timer = 90;
    }

    // Update is called once per frame
    void Update()
    {
        // move "You Win" model upwards
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Mathf.Deg2Rad * timer) * 0.1f, transform.position.z);

        if (timer > 0)
        {
            timer--;
        }
        else if (returnMenuTimer > 0)
        {
            returnMenuTimer--;
        }
        else if (returnMenuTimer <= 0)
        {
            GameObject.Find("Load").GetComponent<Load>().LoadNextScene("TitleScene/Title");
        }
    }
}