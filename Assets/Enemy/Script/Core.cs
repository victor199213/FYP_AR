using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{

    public int hp;
    private Renderer rend;
    public Material[] materials;
    private int textureIndex;
    public int damageToCore;
    [SerializeField]
    GameObject MenuMaker;
    Pausable PausableObjects;
    SpawnManager SpawnManager;
    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        textureIndex = 0;
        PausableObjects = GameObject.Find("PausableObjects").GetComponent<Pausable>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(hp <= 0)
        {
            textureIndex = 1;
        }
        rend.sharedMaterial = materials[textureIndex];
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "Enemy")
        {
            hp -= damageToCore;
        }
        if (col.collider.gameObject.tag == "playerWaypoint" )
        {
            Debug.Log("pause");
            SpawnManager.CancelInvoke();
            Instantiate(MenuMaker);
            PausableObjects.pausing = true;
        }
    }
}
