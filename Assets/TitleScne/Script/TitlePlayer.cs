using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePlayer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("....当たった" + collider.gameObject.tag);

        if (collider.gameObject.tag == "GameStart")
        {
            Debug.Log("Testing");
            SceneManager.LoadScene("GamePlay",LoadSceneMode.Additive);
        }
    }
}