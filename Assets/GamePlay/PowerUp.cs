using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [HideInInspector]
    public int powerUpType;

	// Use this for initialization
	void Start () {
        powerUpType = Random.Range(1, 5);
        switch(powerUpType)
        {
            case 1:
                GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                break;
            case 2:
                GetComponent<Renderer>().material.color = new Color(0, 1, 0);
                break;
            case 3:
                GetComponent<Renderer>().material.color = new Color(0, 0, 1);
                break;
            case 4:
                GetComponent<Renderer>().material.color = new Color(1, 1, 0);
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "turret")
        {
            collision.collider.gameObject.GetComponentInChildren<Shooting>().standardFireRate -= 0.1f;
            //collision.collider.gameObject.transform.position = new Vector3(999, 999, 999);
            Debug.Log(collision.collider.gameObject.name + " " + collision.collider.gameObject.tag);
            Destroy(this.gameObject);
        }
    }
}
