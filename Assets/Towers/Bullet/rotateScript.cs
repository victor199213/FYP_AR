using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScript : MonoBehaviour {

    public GameObject towerTop;

	// Use this for initialization
	void Start () {
        transform.localEulerAngles = new Vector3(0, towerTop.transform.localEulerAngles.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
