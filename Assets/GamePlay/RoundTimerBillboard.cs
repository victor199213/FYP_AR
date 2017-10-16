using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTimerBillboard : MonoBehaviour {

    public GameObject ARCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(this.transform.position - ARCam.transform.position), Time.deltaTime * 1);
	}
}
