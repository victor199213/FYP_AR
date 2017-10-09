using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePlayer : MonoBehaviour
{
    [SerializeField]
    int TapTIme;
    int CntTime =0;
    bool IsStart;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("....当たった" + collider.gameObject.tag);

        if (collider.gameObject.tag == "GameStart")
        {

            CntTime++;
            if (CntTime >= TapTIme)
            {
                if (!IsStart)
                {
                    IsStart = true;
                    GameObject.Find("Load").GetComponent<Load>().LoadNextScene("GamePlay");
                }
                //Instantiate(Cameara);
              
                //Destroy(this.gameObject);
           
            }
        }
        else if (collider.gameObject.tag == "Exit")
        {
            CntTime++;
            if (CntTime >= TapTIme)
            {
                Application.Quit();
            }
        }
        else if (collider.gameObject.tag != "Player")
        {
            CntTime = 0;
        }
      
    }
}