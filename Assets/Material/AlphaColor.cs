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
        rend.material.shader = Shader.Find("Standard");
        GetComponent<Renderer>().material.SetColor("_Color", color);

        rend.material.SetFloat("_Mode", 3);
        rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //rend.material.SetInt("_ZWrite", 0);
        rend.material.DisableKeyword("_ALPHATEST_ON");
        rend.material.DisableKeyword("_ALPHABLEND_ON");
        rend.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        rend.material.renderQueue = 3000;

        rend.material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        rend.material.SetFloat("_SpecularHighlights", 0f);
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
