using System.Collections;
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

    [HideInInspector]
    public bool fireReady = false;

    float standardFireRate;
    float abnormalFireRate = 0.1f;
    string Mytag;
    ExplosiveTowerScript explosiveTowerScript;

    private void Start()
    {
        global = GameObject.FindWithTag("Global");
        standardFireRate = fireRate;
        Mytag = transform.tag;
        explosiveTowerScript = this.gameObject.GetComponent<ExplosiveTowerScript>();
    }

    // Update is called once per frame
    void Update ()
    {
        fireTimer += Time.deltaTime;

        nearTarget = FindClosestPlayer();

        float dis = Vector3.Distance(nearTarget.transform.position, this.transform.position);

        if (fireTimer >= fireRate && fireReady == true && (nearTarget.transform.position != Vector3.zero))
        {
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(nearTarget.transform.position - transform.position));
            if(angle < fieldOfView)
            {
                if (dis < detectionDistance)
                {
                    SpawnBullet();
                    fireTimer = 0;
                    if(anim)
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
            //anim.SetInteger("state", 0);
        }

        if (Mytag == "Normal")
        {
            if (GameObject.Find("Poison") != null)
            {
                Debug.Log(Vector3.Distance(GameObject.Find("Poison").transform.position, this.transform.position));

            }
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
            lastBullet.Add(tempBullet);
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
            else if(col.collider.gameObject.gameObject.tag == "Normal")
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
            
            this.gameObject.GetComponent<ParticleSystem>().Stop();
            explosiveTowerScript.ChangeDamage(true);
        }
        else
        {
            fireRate = abnormalFireRate;
            explosiveTowerScript.ChangeDamage(false);
            this.gameObject.GetComponent<ParticleSystem>().Play();
        }   
    }
}
