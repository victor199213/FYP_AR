using UnityEngine;
using System.Collections;

public class EnemyRange : MonoBehaviour
{
    enum FSM
    {
        OBJECTIVE,
        AGGRO,
        CHASE,
        ATTACK,
        DEAD
    };

    public UnityEngine.AI.NavMeshAgent agentObjective;
    public GameObject coreObjective;
    //public int hp;
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

    void Start()
    {
        agentObjective = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyState = FSM.OBJECTIVE;
        playerAble = GameObject.FindWithTag(attackTag);
        poisoned = false;
        poisonTimer = 0.0f;
    }

    void Update()
    {
        dis = Vector3.Distance(this.transform.position, playerAble.transform.position);
        switch (enemyState)
        {
            case FSM.OBJECTIVE:
                Objective();
                break;
            case FSM.AGGRO:
                Aggro();
                break;;
            case FSM.ATTACK:
                Attack();
                break;
            case FSM.DEAD:
                Dead();
                break;

        };

        if (poisoned == true)
        {
            poisonTimer += Time.deltaTime;
            if (poisonTimer >= 1.0f)
            {
                hp -= 1;
                poisonTimer -= 1.0f;
            }
        }

        if (hp <= 0)
        {
            enemyState = FSM.DEAD;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == coreObjective.name)
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
            if (enemyState == FSM.OBJECTIVE)
            {
                enemyState = FSM.AGGRO;
            }
        }

    }

    void OnCollisionStay(Collision col)
    {
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
        agentObjective.isStopped = false;
        agentObjective.SetDestination(coreObjective.transform.position);
    }

    void Aggro()
    {
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
        EnemyShooting shoot = this.GetComponent<EnemyShooting>();
        shoot.fireReady = true;
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
        if (dis > disengageDistance)
        {
            enemyState = FSM.OBJECTIVE;
            shoot.fireReady = false;
        }
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }

    GameObject FindClosestPlayer(GameObject closestPlayer)
    {
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

