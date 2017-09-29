using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCursor : MonoBehaviour {

    public Transform goal;
    public int holdTime;
    private float timer;
    private int currentHoverButton;
    // 0 = none, 1 = play, 2 = exit

    // Use this for initialization
    void Start () {
        currentHoverButton = 0;
        if(holdTime > 0)
        {
            timer = holdTime;
        }
        else
        {
            timer = 3.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = goal.transform.position;
        if(currentHoverButton != 0)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0.0f)
        {
            switch(currentHoverButton)
            {
                case 1:
                    Application.LoadLevel("GamePlay");
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
	}

    private void OnCollisionStay(Collision col)
    {
        if (col.collider.gameObject.name == "playButton")
        {
            if (currentHoverButton != 1)
            {
                if (holdTime > 0)
                {
                    timer = holdTime;
                }
                else
                {
                    timer = 3.0f;
                }
            }
            currentHoverButton = 1;
        }
        if (col.collider.gameObject.name == "quitButton")
        {
            if(currentHoverButton != 2)
            {
                if (holdTime > 0)
                {
                    timer = holdTime;
                }
                else
                {
                    timer = 3.0f;
                }
            }
            currentHoverButton = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        currentHoverButton = 0;
        if (holdTime > 0)
        {
            timer = holdTime;
        }
        else
        {
            timer = 3.0f;
        }
    }
}
