using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2Shooting : MonoBehaviour
{
    public float fireRate;
    public float fieldOfView;
    public GameObject explodingBullet;
    public GameObject target;
    public List<GameObject> bulletSpawn;
    float fireTimer;
    List<GameObject> lastBullet = new List<GameObject>();
    GameObject nearTarget = null;
    GameObject global;

    [HideInInspector]
    public bool fireReady = false;

    private void Start()
    {
        global = GameObject.FindWithTag("Global");
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;

        nearTarget = FindClosestPlayer(target);

        float dis = Vector3.Distance(nearTarget.transform.position, this.transform.position);

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

    void SpawnBullet()
    {
        if (!explodingBullet)
        {
            return;
        }

        lastBullet.Clear();

        for (int i = 0; i < bulletSpawn.Count; i++)
        {
            GameObject tempBullet = Instantiate(explodingBullet, bulletSpawn[i].transform.position, Quaternion.Euler(bulletSpawn[i].transform.forward)) as GameObject;
            tempBullet.GetComponent<BaseBullet>().FireBullet(bulletSpawn[i], nearTarget);
            tempBullet.transform.parent = global.transform;
            lastBullet.Add(tempBullet);
            fireReady = false;
        }
    }


    GameObject FindClosestPlayer(GameObject closestPlayer)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
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
