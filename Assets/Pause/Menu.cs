using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    GameObject MenuMaker;
    Pausable PausableObjects;
    SpawnManager SpawnManager;
    bool toggle;

    // Use this for initialization
    void Start () {
        PausableObjects = GameObject.Find("PausableObjects").GetComponent<Pausable>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        MenuMaker = GameObject.Find("Pause");
        toggle = true;

    }
    
    // Update is called once per frame
    void Update () {

    }
    

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.gameObject.tag == "coreObjective")
        {
            SpawnManager.CancelInvoke();
            MenuMaker.transform.localScale = new Vector3(1, 1, 1);
            PausableObjects.pausing = true;
            Time.timeScale = 0.1f;
            //Instantiate(MenuMaker);
        }
        if (col.gameObject.gameObject.tag == "Resume")
        {
            MenuMaker.transform.localScale = new Vector3(0, 0, 0);
            PausableObjects.pausing = false;
            Time.timeScale = 1.0f;
        }
        if (col.gameObject.gameObject.tag == "TitleButton")
        {
            GameObject.Find("Load").GetComponent<Load>().LoadNextScene("TitleScene/Title");
         
        }

    }
}
