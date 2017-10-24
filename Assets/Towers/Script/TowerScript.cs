using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TowerScript : MonoBehaviour
{

    public float hp;
    [HideInInspector]
    public float maxHP;
    public int firerate;
    public int damage;
    FSM turretState;
    public int towerType;
    public GameObject goal;
    public GameObject tracker;

    public GameObject towerTop;
    public Mesh damaged;
    public Mesh destroyed;

    public GameObject aimTracking;

    public Animator anim;
    [HideInInspector]
    public int standardDamage;
    int abnormalDamage = 10;

    enum FSM
    {
        IDLE,
        ATTACK,
        DEAD
    }

    void Start()
    {
        turretState = FSM.IDLE;
        maxHP = hp;
        disableParticles();
        standardDamage = damage;

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
            towerTop.GetComponent<SkinnedMeshRenderer>().sharedMesh = destroyed;
            anim.SetInteger("state", 2);
            turretState = FSM.DEAD;
            Shooting shoot = this.GetComponent<Shooting>();
            shoot.fireReady = false;
            aimTracking.GetComponent<TrackingSystem>().enabled = false;
        }
        else if (hp <= maxHP / 2)
        {
            towerTop.GetComponent<SkinnedMeshRenderer>().sharedMesh = damaged;
        }

        if (hp > maxHP)
        {
            hp = maxHP;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == "EnemyBullet")
        {
            GameObject enemyRange = GameObject.FindWithTag("EnemyBullet");
            EnemyBullet tempEnemyRange = enemyRange.GetComponent<EnemyBullet>();
            hp -= tempEnemyRange.damage;
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
        this.GetComponent<Shooting>().fireReady = false;
        anim.SetInteger("state", 0);
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
            hp += player.GetComponent<Player>().healingRate;
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

    public void ChangeDamage(bool standard)
    {
        if (standard)
        {
            damage = standardDamage;
        }
        else
        {
            damage = abnormalDamage;
        }
    }
}