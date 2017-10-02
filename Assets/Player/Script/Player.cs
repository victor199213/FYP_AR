using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    enum FSM
    {
        MOVE,
        ATTACK,

    };

    UnityEngine.AI.NavMeshAgent agent;
    public Transform goal;
    public int hp;
    public int damage;
    FSM state;
    public int healRange;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        state = FSM.MOVE;
    }

    void Update()
    {
        switch (state)
        {
            case FSM.MOVE:
                Move();
                break;
            case FSM.ATTACK:
                Attack();
                break;
        }
    }

    void Move()
    {
        agent.SetDestination(goal.transform.position);
        Physics.IgnoreCollision(goal.GetComponent<Collider>(), GetComponent<Collider>());
        if (Vector3.Distance(this.gameObject.transform.position, goal.transform.position) < 10)
        {
            state = FSM.ATTACK;
        }
    }

    void Attack()
    {
        Shooting shoot = this.GetComponent<Shooting>();
        shoot.fireReady = true;
        if (Vector3.Distance(this.gameObject.transform.position, goal.transform.position) > 5)
        {
            state = FSM.MOVE;
            shoot.fireReady = false;
        }
    }
}