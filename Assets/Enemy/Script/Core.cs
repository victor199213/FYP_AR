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
    private int coreStage2;
    private int coreStage3;

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        textureIndex = 0;
        PausableObjects = GameObject.Find("PausableObjects").GetComponent<Pausable>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        coreStage2 = (hp / 100) * 70;
        coreStage3 = (hp / 100) * 30;
    }
	
	// Update is called once per frame
	void Update ()
    { 
		if(hp <= coreStage3)
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
            PausableObjects.pausing = true;
            Instantiate(MenuMaker);
        }
    }
}
