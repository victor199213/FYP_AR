﻿using UnityEngine;
using System.Collections;

public class EnemyRange : MonoBehaviour
{
    //State machine variable

    enum FSM
    {
        OBJECTIVE,
        AGGRO,
        CHASE,
        ATTACK,
        DEAD
    };

    //Define variable

    public UnityEngine.AI.NavMeshAgent agentObjective;
    public GameObject coreObjective;
    public float hp;
    public int damage;
    FSM enemyState;
    GameObject playerAble;
    public float attackDistance;
    public float disengageDistance;
    public string attackTag;
    float dis;
    Vector3 m_lastKnownPosition = Vector3.zero;
    GameObject nearTarget = null;
    Quaternion m_lookAtRotation;
    public float aggroDuration;
    private float tempTime;
    public float trackingSpeed;
    private bool poisoned;
    private float poisonTimer;
    public int deathTimer; // For death animation
    float resetTimer;
    Animator anim;
    public int powerUpDropChance = 20;
    public GameObject powerUp;
    private GameObject Terrain;

    void Start()
    {
        //initializing variable

        agentObjective = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyState = FSM.OBJECTIVE;
        playerAble = GameObject.FindWithTag(attackTag);
        poisoned = false;
        poisonTimer = 0.0f;
        resetTimer = 0;
        anim = gameObject.GetComponent<Animator>();
        Terrain = GameObject.FindGameObjectWithTag("Global");
    }

    void Update()
    {
        //Getting distance between enemy and turret position

        dis = Vector3.Distance(this.transform.position, playerAble.transform.position);


        //state machine controller
        switch (enemyState)
        {
                case FSM.OBJECTIVE:
                    Objective();
                    break;
                case FSM.AGGRO:
                    Aggro();
                    break; ;
                case FSM.ATTACK:
                    Attack();
                    break;
                case FSM.DEAD:
                    Dead();
                    break;

        };
        //damage overtime when poisoned

        if (poisoned == true)
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer >= 1.0f)
            {
                hp -= 1;
                poisonTimer -= 1.0f;
            }
        }

        //check enemy still has HP
        if (hp <= 0)
        {
            enemyState = FSM.DEAD;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //Collision checking

        if (col.gameObject.tag == "coreObjective")
        {
            Destroy(this.gameObject);
        }

        if (col.collider.gameObject.tag == "NormalTowerBullet")
        {
            GameObject tower = GameObject.FindWithTag("Normal");
            TowerScript tempTower = tower.GetComponent<TowerScript>();
            hp -= tempTower.damage;
            if (enemyState == FSM.OBJECTIVE)
            {
                enemyState = FSM.AGGRO;
            }
        }
        if (col.collider.gameObject.tag == "ExplosiveTowerBullet")
        {
            GameObject tower = GameObject.FindWithTag("Explosive");
            ExplosiveTowerScript tempTower = tower.GetComponent<ExplosiveTowerScript>();
            hp -= tempTower.damage;
            if (enemyState == FSM.OBJECTIVE)
            {
                enemyState = FSM.AGGRO;
            }
        }
        if (col.collider.gameObject.tag == "DoTTowerBullet")
        {
            GameObject tower = GameObject.FindWithTag("Poison");
            PoisonTowerScript tempTower = tower.GetComponent<PoisonTowerScript>();
            hp -= tempTower.damage;
            poisonHit();
            if (enemyState == FSM.OBJECTIVE)
            {
                enemyState = FSM.AGGRO;
            }
        }
        if (col.collider.gameObject.tag == "Bullet")
        {
            GameObject player = GameObject.FindWithTag("Player");
            Player tempPlayer = player.GetComponent<Player>();
            hp -= tempPlayer.damage;
        }

    }

    void OnCollisionStay(Collision col)
    {
        //Collision checking

        if (col.collider.gameObject.tag == "ExplosiveTowerBullet")
        {
            GameObject tower = GameObject.FindWithTag("Explosive");
            ExplosiveTowerScript tempTower = tower.GetComponent<ExplosiveTowerScript>();
            hp -= tempTower.damage;
            if (enemyState == FSM.OBJECTIVE)
            {
                enemyState = FSM.AGGRO;
            }
        }
    }

    void Objective()
    {
        //Moving enemy towards the objective

        anim.SetBool("Aggro", false);
        anim.SetBool("Walk", true);
        resetTimer = 0;
        agentObjective.isStopped = false;
        agentObjective.SetDestination(coreObjective.transform.position);
    }

    void Aggro()
    {
        //Play animation when an turret detected and rotate to face the turret

        anim.SetBool("Aggro", true);
        tempTime += Time.deltaTime;

        agentObjective.isStopped = true;

        GameObject player = GameObject.FindWithTag(attackTag);
        nearTarget = FindClosestPlayer(player);
        playerAble = nearTarget;

        if (dis < disengageDistance)
        {
            if (m_lastKnownPosition != nearTarget.transform.position)
            {
                m_lastKnownPosition = nearTarget.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, trackingSpeed * Time.deltaTime);
            }
            if (aggroDuration <= tempTime)
            {
                enemyState = FSM.ATTACK;
                tempTime = 0;
            }
        }
        if (dis > disengageDistance)
        {
            enemyState = FSM.OBJECTIVE;
        }
    }

    void Attack()
    {
        //Shoot the turret when near it

        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);

        EnemyShooting shoot = this.GetComponent<EnemyShooting>();
        shoot.fireReady = true;
        resetTimer += Time.deltaTime * 1;
        if (dis < disengageDistance)
        {
            if (m_lastKnownPosition != nearTarget.transform.position)
            {
                m_lastKnownPosition = nearTarget.transform.position;
                m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            }

            if (transform.rotation != m_lookAtRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, trackingSpeed * Time.deltaTime);
            }
        }
        if (dis > disengageDistance || resetTimer >= 10)
        {
            enemyState = FSM.OBJECTIVE;
            shoot.fireReady = false;
        }
    }

    void Dead()
    {
        //Play animation when dead

        EnemyShooting shoot = this.GetComponent<EnemyShooting>();
        agentObjective.isStopped = false;
        agentObjective.speed = 0;
        shoot.fireReady = true;
        shoot.fireReady = false;
        anim.SetBool("Dead", true);
        deathTimer -= 1;
        if(deathTimer <= 0)
        {
            if (powerUp && Terrain && Random.Range(1, powerUpDropChance) == 1)
            {
                Instantiate(powerUp, transform.position, transform.rotation, Terrain.transform);
            }
            Destroy(this.gameObject);
        }
    }

    GameObject FindClosestPlayer(GameObject closestPlayer)
    {
        //find the closest turret

        GameObject[] gos = GameObject.FindGameObjectsWithTag(attackTag);
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPlayer = go;
                distance = curDistance;
            }
        }
        return closestPlayer;
    }

    public void poisonHit()
    {
        poisoned = true;
    }

    public int getFSM()
    {
        switch(enemyState)
        {
            case FSM.OBJECTIVE:
                return 0;

            case FSM.AGGRO:
                return 1;

            case FSM.CHASE:
                return 2;

            case FSM.ATTACK:
                return 3;

            case FSM.DEAD:
                return 4;

        }
        return -1;
    }
}

