using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour {

    [SerializeField]
    Shooting NormalTowerShooting;
    [SerializeField]
    Shooting ExplosiveTowerShooting;
    [SerializeField]
    Shooting DoTTowerShooting;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //private void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.gameObject.tag == "Resume")
    //}

}
