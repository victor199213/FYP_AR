using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePlayer : MonoBehaviour
{
    [SerializeField]
    int TapTIme;
    [SerializeField]
    float CntTime = 0.0f;
    bool IsStart;
    // Use this for initialization
    void Start()
    {
        CntTime = 0;
        Time.timeScale = 1.0f;
        IsStart = false;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay(Collider collider)
    {
        if (!IsStart)
        {
            Debug.Log("....当たった" + collider.gameObject.tag);

            if (collider.gameObject.tag == "GameStart")
            {

                CntTime += 1.0f * Time.deltaTime;
                if (CntTime >= TapTIme)
                {

                    IsStart = true;
                    GameObject.Find("Load").GetComponent<Load>().LoadNextScene("GamePlay");

                    //Instantiate(Cameara);

                    //Destroy(this.gameObject);
                    //SceneManager.LoadScene("GamePlay");
                }
            }
            else if (collider.gameObject.tag == "Exit")
            {
                CntTime += 1.0f * Time.deltaTime; ;
                if (CntTime >= TapTIme)
                {
                    Application.Quit();
                }
            }
            else if (collider.gameObject.tag != "Player")
            {
                CntTime = 0.0f;
            }

        }
    }
}