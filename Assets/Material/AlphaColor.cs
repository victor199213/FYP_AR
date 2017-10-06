using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaColor : MonoBehaviour
{
    private Color color;
    private Renderer rend;

    void Start ()
    {
        color = GetComponent<Renderer>().material.color;
        color.a -= 0.25f;
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Transparent/Diffuse");
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    private void Update()
    {
        if (this.gameObject.tag == "Wall")
        {
            this.rend.enabled = false;
        }
        if (this.gameObject.tag == "coreObjective")
        {
            this.rend.enabled = false;
        }
    }
}
