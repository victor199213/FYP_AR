using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constraint : MonoBehaviour {

    // Use this for initialization
    public float height;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        //transform.Rotate(new Vector3(0,0,0));
    }
}
