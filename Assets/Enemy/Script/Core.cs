﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    //State machine variable
    public int hp;
    private Renderer rend;
    public Material[] materials;
    private int textureIndex;
    public int damageToCore;
    private int coreStage1;
    private int coreStage2;
    private int coreStage3;
    public GameObject losePopup;
    public GameObject spawnManager;
    [HideInInspector]
    public bool gameLose;

    // Use this for initialization
    void Start ()
    {
        //Define variable
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        textureIndex = 0;
        coreStage1 = (hp / 100) * 100;
        coreStage2 = (hp / 100) * 70;
        coreStage3 = (hp / 100) * 30;
        gameLose = false;

    }

    // Update is called once per frame
    void Update ()
    { 
        //changing the core color when damage

		if(hp <= coreStage1 && hp >= coreStage2)
        {
            textureIndex = 0;
            rend.sharedMaterial = materials[textureIndex];
        }
        if (hp <= coreStage2 && hp >= coreStage3)
        {
            textureIndex = 1;
            rend.sharedMaterial = materials[textureIndex];
        }
        if (hp <= coreStage3)
        {
            textureIndex = 2;
            rend.sharedMaterial = materials[textureIndex];
        }

        //if core HP is zero or less you lose
        if(hp <= 0)
        {
            if (spawnManager.GetComponent<SpawnManager>().gameWin == false && gameLose == false)
            {
                gameLose = true;
                Instantiate(losePopup);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Collision checking 
        if (col.collider.gameObject.tag == "Enemy")
        {
            hp -= damageToCore;
        }
    }
}
