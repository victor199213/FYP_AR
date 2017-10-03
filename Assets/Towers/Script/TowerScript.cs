﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TowerScript : MonoBehaviour
{

    public float hp;
    private float maxHP;
    public int firerate;
    public int damage;
    private float timer;
    FSM turretState;
    public int towerType;
    public GameObject goal;
    public GameObject tracker;

    enum FSM
    {
        IDLE,
        ATTACK,
        DEAD
    }

    void Start()
    {
        turretState = FSM.IDLE;
        timer = 0;
        maxHP = hp;
        disableParticles();
    }

    void Update()
    {
        switch (turretState)
        {
            case FSM.IDLE:
                detectEnemy();
                break;
            case FSM.ATTACK:
                Attack();
                break;
            case FSM.DEAD:
                break;
        }

        if (tracker.GetComponent<TrackableBehaviour>().CurrentStatus == TrackableBehaviour.Status.TRACKED && goal.GetComponent<TargetInsideWall>().insideWall == false)
        {
            snap();
        }

        detectPlayer();

        if (hp <= 0)
        {
            turretState = FSM.DEAD;
        }

        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }

    void Attack()
    {
        Shooting shoot = this.GetComponent<Shooting>();
        shoot.fireReady = true;
        detectEnemy();
    }

    void detectEnemy()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < this.GetComponent<Shooting>().detectionDistance)
            {
                turretState = FSM.ATTACK;
                return;
            }
        }
        turretState = FSM.IDLE;
    }

    void snap()
    {
        this.transform.parent.position = new Vector3(goal.transform.position.x, this.transform.parent.position.y, goal.transform.position.z);
    }

    void detectPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 diff = player.transform.position - transform.position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < player.GetComponent<Player>().healRange)
        {
            hp += 0.01f;
            return;
        }
    }

    void enableParticles()
    {
        if (this.gameObject.GetComponent<ParticleSystem>().isPlaying == false)
        {
            this.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }

    void disableParticles()
    {
        this.gameObject.GetComponent<ParticleSystem>().Stop();
    }
}