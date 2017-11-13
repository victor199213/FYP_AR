using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTextAppear : MonoBehaviour {

    public GameObject warning1;
    public GameObject warning2;
    public GameObject warning3;
    public GameObject warning4;
    
    public float fadeTime;
    public int numberOfFlashes;

    private int flashCounter;
    private bool triggered;

	// Use this for initialization
	void Start () {
        warning1.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        warning2.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        warning3.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        warning4.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        flashCounter = numberOfFlashes * 2;
        triggered = false;
        triggerWarning();
    }
	
	// Update is called once per frame
	void Update () {
		if(triggered == true)
        {
            if(flashCounter > 0)
            {
                if(flashCounter % 2 == 0)
                {
                    if(warning1.GetComponent<TextMesh>().color.a >= 1.0f)
                    {
                        flashCounter -= 1;
                    }
                    else
                    {
                        warning1.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a + ((1.0f / fadeTime) * Time.deltaTime));
                        warning2.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a + ((1.0f / fadeTime) * Time.deltaTime));
                        warning3.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a + ((1.0f / fadeTime) * Time.deltaTime));
                        warning4.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a + ((1.0f / fadeTime) * Time.deltaTime));
                    }
                }
                else
                {
                    if (warning1.GetComponent<TextMesh>().color.a <= 0.0f)
                    {
                        flashCounter -= 1;
                    }
                    else
                    {
                        warning1.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a - ((1.0f / fadeTime) * Time.deltaTime));
                        warning2.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a - ((1.0f / fadeTime) * Time.deltaTime));
                        warning3.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a - ((1.0f / fadeTime) * Time.deltaTime));
                        warning4.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, warning1.GetComponent<TextMesh>().color.a - ((1.0f / fadeTime) * Time.deltaTime));
                    }
                }
            }
            else
            {
                triggered = false;
            }
        }
    }

    private void triggerWarning()
    {
        triggered = true;
        flashCounter = numberOfFlashes * 2;
    }
}
