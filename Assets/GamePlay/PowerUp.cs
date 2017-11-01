using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [HideInInspector]
    public int powerUpType;

    public Mesh damageModel;
    public Mesh HPModel;
    public Mesh rangeModel;
    public Mesh speedModel;

    public Material damageMat;
    public Material HPMat;
    public Material rangeMat;
    public Material speedMat;

	// Use this for initialization
	void Start () {
        powerUpType = Random.Range(1, 5);
        switch (powerUpType)
        {
            case 1:
                GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = damageModel;
                transform.GetChild(0).transform.localPosition = new Vector3(0, 0, -33.4f);
                GetComponentInChildren<Renderer>().material = damageMat;
                break;
            case 2:
                GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = HPModel;
                transform.GetChild(0).transform.localPosition = new Vector3(0, 0, -29.2f);
                GetComponentInChildren<Renderer>().material = HPMat;
                break;
            case 3:
                GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = rangeModel;
                transform.GetChild(0).transform.localPosition = new Vector3(0, 0, -37.3f);
                GetComponentInChildren<Renderer>().material = rangeMat;

                break;
            case 4:
                GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = speedModel;
                transform.GetChild(0).transform.localPosition = new Vector3(0, 0, -41.8f);
                GetComponentInChildren<Renderer>().material = speedMat;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0.5f, 0);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.tag == "turret")
    //    {
    //        collision.collider.gameObject.GetComponentInChildren<Shooting>().standardFireRate -= 0.1f;
    //        //collision.collider.gameObject.transform.position = new Vector3(999, 999, 999);
    //        Debug.Log(collision.collider.gameObject.name + " " + collision.collider.gameObject.tag);
    //        Destroy(this.gameObject);
    //    }
    //}
}
