using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    GameObject MenuMaker;
    Pausable PausableObjects;
    SpawnManager SpawnManager;
    bool toggle;
    Renderer rend;
    MeshRenderer TitleButton;
    MeshRenderer ResumeButton;
    // Use this for initialization
    void Start () {
        PausableObjects = GameObject.Find("PausableObjects").GetComponent<Pausable>();
        SpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        MenuMaker = GameObject.Find("Pause");
        toggle = true;
        rend = GetComponent<Renderer>();
        TitleButton = GameObject.Find("TitleCube").GetComponent<MeshRenderer>();
        ResumeButton = GameObject.Find("ResumeCube").GetComponent<MeshRenderer>();
    }
    
    // Update is called once per frame
    void Update () {

    }
    

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.gameObject.tag == "coreObjective")
        {
            SpawnManager.CancelInvoke();
            TitleButton.enabled = true;
            ResumeButton.enabled = true;
            PausableObjects.pausing = true;
            Time.timeScale = 0.1f;
            //Instantiate(MenuMaker);
        }
        if (col.gameObject.gameObject.tag == "Resume")
        {
            TitleButton.enabled = false;
            ResumeButton.enabled = false;
            PausableObjects.pausing = false;
            Time.timeScale = 1.0f;
        }
        if (col.gameObject.gameObject.tag == "TitleButton")
        {
            GameObject.Find("Load").GetComponent<Load>().LoadNextScene("TitleScene/Title");
         
        }

    }
}
