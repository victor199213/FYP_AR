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
    public float healingRate;

    public Animator anim;

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
        agent.destination = goal.transform.position;
        Physics.IgnoreCollision(goal.GetComponent<Collider>(), GetComponent<Collider>());
        if (Vector3.Distance(this.gameObject.transform.position, goal.transform.position) < 2)
        {
            state = FSM.ATTACK;
            anim.SetInteger("state", 0);
        }
    }

    void Attack()
    {
        Shooting shoot = this.GetComponent<Shooting>();
        shoot.fireReady = true;
        if (Vector3.Distance(this.gameObject.transform.position, goal.transform.position) > 1)
        {
            state = FSM.MOVE;
            shoot.fireReady = false;
            anim.SetInteger("state", 2);
        }
    }
}