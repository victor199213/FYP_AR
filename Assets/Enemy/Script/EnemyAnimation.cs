using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity != Vector3.zero)
        {
            anim.SetInteger("state", 5);
            //Debug.Log(transform.parent.transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance);
        }
        else
        {
            if (transform.parent.transform.parent.GetComponent<Enemy>() != null)
            {
                anim.SetInteger("state", transform.parent.transform.parent.GetComponent<Enemy>().getFSM());
            }
            else
            {
                anim.SetInteger("state", transform.parent.transform.parent.GetComponent<EnemyRange>().getFSM());
            }
        }
	}

    /*
    0 = OBJECTIVE
    1 = AGGRO
    2 = CHASE
    3 = ATTACK
    4 = DEAD
    */
}
