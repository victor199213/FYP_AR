using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeRenderCylinder : MonoBehaviour {

    private int timer = 2;
	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(timer > 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            timer--;
        }
    }
}
