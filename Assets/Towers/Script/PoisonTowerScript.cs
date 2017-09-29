using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTowerScript : MonoBehaviour
{

    public int hp;
    public int firerate;
    public int damage;
    private float timer;
    FSM turretState;
    public int towerType;

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

        if (hp < 0)
        {
            turretState = FSM.DEAD;
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
}
