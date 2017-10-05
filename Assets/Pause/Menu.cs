using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField]
    GameObject MenuMaker;
    Pausable PausableObjects;
    SpawnManager SpawnManager;
    // Use this for initialization
    void Start () {
        PausableObjects = GameObject.Find("PausableObjects").GetComponent<Pausable>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision col)
    {
     
        if (col.collider.gameObject.tag == "coreObjective")
        {
            Debug.Log("pause");
            SpawnManager.CancelInvoke();
            PausableObjects.pausing = true;
            Instantiate(MenuMaker);
        }
        if (col.collider.gameObject.tag == "Exit")
        {
            Application.Quit();
        }
        if (col.collider.gameObject.tag == "TitleButton")
        {
            SceneManager.LoadScene("TitleScene");
        }

    }
}
