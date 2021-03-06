﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float fireRate;
    public float fieldOfView;
    public GameObject bullet;
    public List<GameObject> bulletSpawn;
    public int detectionDistance;
    float fireTimer;
    List<GameObject> lastBullet = new List<GameObject>();
    GameObject nearTarget = null;
    GameObject global;
    public Animator anim;
    GameObject closestPlayer;
    private float dis;

    [HideInInspector]
    public bool fireReady = false;

    [HideInInspector]
    public float standardFireRate;
    float abnormalFireRate = 0.3f;
    string Mytag;
    PoisonTowerScript poisonTowerScript;
    ExplosiveTowerScript explosiveTowerScript;
    TowerScript towerScript;

    public GameObject towerTop;

    private void Start()
    {
        global = GameObject.FindWithTag("Global");
        standardFireRate = fireRate;
        Mytag = transform.tag;
        switch (Mytag)
        {
            case "Poison":
                poisonTowerScript = this.gameObject.GetComponent<PoisonTowerScript>();
                break;
            case "Explosive":
                explosiveTowerScript = this.gameObject.GetComponent<ExplosiveTowerScript>();
                break;
            case "Normal":
                towerScript = this.gameObject.GetComponent<TowerScript>();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        fireTimer += Time.deltaTime;

        nearTarget = FindClosestPlayer();

        if(GameObject.FindWithTag("Enemy") != null)
        {
            dis = Vector3.Distance(nearTarget.transform.position, this.transform.position);

            if (fireTimer >= fireRate && fireReady == true && (nearTarget.transform.position != Vector3.zero))
            {
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(nearTarget.transform.position - transform.position));
                if (angle < fieldOfView)
                {
                    if (dis < detectionDistance)
                    {
                        SpawnBullet();
                        fireTimer = 0;
                        if (anim)
                        {
                            anim.SetInteger("state", 1);
                        }
                    }
                    else
                    {
                        anim.SetInteger("state", 0);
                    }
                }
            }
            else if (anim && fireTimer < fireRate)
            {
                if(tag != "Player")
                {
                    anim.SetInteger("state", 0);
                }
            }

            if (Mytag == "Normal")
            {
                if (GameObject.Find("Poison") != null)
                {
                    Debug.Log(Vector3.Distance(GameObject.Find("Poison").transform.position, this.transform.position));

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
        if(!bullet)
        {
            return;
        }

        lastBullet.Clear();

        for (int i = 0; i < bulletSpawn.Count; i++)
        {
            GameObject tempBullet = Instantiate(bullet, bulletSpawn[i].transform.position, Quaternion.Euler(bulletSpawn[i].transform.forward)) as GameObject;
            tempBullet.GetComponent<BaseBullet>().FireBullet(bulletSpawn[i], nearTarget);
            tempBullet.transform.parent = global.transform;
            if(towerTop)
                tempBullet.transform.eulerAngles = new Vector3(0, towerTop.transform.localEulerAngles.y, 0);
            lastBullet.Add(tempBullet);
            if(tag == "Normal")
                Debug.Log("this: " + towerTop.transform.eulerAngles + " bullet: " + tempBullet.transform.eulerAngles);
        }
    }


    GameObject FindClosestPlayer()
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



    void OnCollisionStay(Collision col)
    {
        if (Mytag != col.collider.gameObject.tag)
        {
            if (col.collider.gameObject.gameObject.tag == "Poison")
                ChangeFireRate(false);
            else if (col.collider.gameObject.gameObject.tag == "Explosive")
                ChangeFireRate(false);
            else if (col.collider.gameObject.gameObject.tag == "Normal")
                ChangeFireRate(false);
            else if (col.collider.gameObject.gameObject.tag != "Untagged")
                ChangeFireRate(true);
        }
    }

    public void ChangeFireRate(bool standard)
    {
        
   
        if (standard)
        {
            fireRate = standardFireRate;

            switch (Mytag)
            {
                case "Poison":
                    poisonTowerScript.ChangeDamage(true);
                    this.gameObject.GetComponent<ParticleSystem>().Stop();
                    break;
                case "Explosive":
                    explosiveTowerScript.ChangeDamage(true);
                    this.gameObject.GetComponent<ParticleSystem>().Stop();
                    break;
                case "Normal":
                    towerScript.ChangeDamage(true);
                    this.gameObject.GetComponent<ParticleSystem>().Stop();
                    break;
                default:
                    break;

            }
        }
        else
        {
            fireRate = abnormalFireRate;
            switch (Mytag)
            {
                case "Poison":
                    poisonTowerScript.ChangeDamage(false);
                    this.gameObject.GetComponent<ParticleSystem>().Play();
                    break;
                case "Explosive":
                    explosiveTowerScript.ChangeDamage(false);
                    this.gameObject.GetComponent<ParticleSystem>().Play();
                    break;
                case "Normal":
                    towerScript.ChangeDamage(false);
                    this.gameObject.GetComponent<ParticleSystem>().Play();
                    break;
                default:
                    break;

            }
        }
    }
}
