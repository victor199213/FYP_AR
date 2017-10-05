using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    GameObject MenuMaker;
    Pausable PausableObjects;
    SpawnManager SpawnManager;
    // Use this for initialization
    void Start () {
        PausableObjects = GameObject.Find("PausableObjects").GetComponent<Pausable>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        MenuMaker = GameObject.Find("Pause");
    }
    
    // Update is called once per frame
    void Update () {
		
	}
    

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.gameObject.tag == "coreObjective")
        {
            Debug.Log("pause");
            SpawnManager.CancelInvoke();
            MenuMaker.transform.localScale = new Vector3(1, 1, 1);
            PausableObjects.pausing = true;
            //Instantiate(MenuMaker);
        }
        if (col.gameObject.gameObject.tag == "Resume")
        {
            MenuMaker.transform.localScale = new Vector3(0, 0, 0);
            PausableObjects.pausing = false;

        }
        if (col.gameObject.gameObject.tag == "TitleButton")
        {
            SceneManager.LoadScene("TitleScene/Title");
        }

    }
}
