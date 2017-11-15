using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //Define variable
    public float fireRate;
    public float fieldOfView;
    public GameObject bullet;
    private GameObject target;
    public List<GameObject> bulletSpawn;
    public string attackTag;
    float fireTimer;
    List<GameObject> lastBullet = new List<GameObject>();
    GameObject nearTarget = null;
    GameObject global;
    private float dis;
    [HideInInspector]
    public bool fireReady = false;

    private void Start()
    {
        //initializing variable
        global = GameObject.FindWithTag("Global");
        target = GameObject.FindWithTag(attackTag);
    }

    // Update is called once per frame
    void Update()
    {
        //enemy will when all condition is met and will rotate to face the turret

        fireTimer += Time.deltaTime;
        nearTarget = FindClosestPlayer(target);

        if (GameObject.FindWithTag("Enemy") != null)
        {
            dis = Vector3.Distance(nearTarget.transform.position, this.transform.position);
            if (fireTimer >= fireRate && fireReady == true)
            {
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position));
                if (angle < fieldOfView)
                {
                    if (dis < 10)
                    {
                        SpawnBullet();
                        fireTimer = 0;
                    }
                }
            }
        }
        else
        {
            dis = 0;
        }
    }

    void SpawnBullet()
    {
        //spawn bullet when shooting 
        if (!bullet)
        {
            return;
        }

        lastBullet.Clear();

        for (int i = 0; i < bulletSpawn.Count; i++)
        {
            GameObject tempBullet = Instantiate(bullet, bulletSpawn[i].transform.position, Quaternion.Euler(bulletSpawn[i].transform.forward)) as GameObject;
            tempBullet.GetComponent<BaseBullet>().FireBullet(bulletSpawn[i], nearTarget);
            tempBullet.transform.parent = global.transform;
            lastBullet.Add(tempBullet);
        }
    }


    GameObject FindClosestPlayer(GameObject closestPlayer)
    {
        //find Closest Player to the turret
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
}

