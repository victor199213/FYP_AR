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

        //constraint the height of the object
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
