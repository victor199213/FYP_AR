using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseBullet
{

    Vector3 direction;
    bool fired;
    float destroyTimer;
    public string attackTag;
    public string turretTag;

    void Update()
    {
        destroyTimer += Time.deltaTime;
        if (fired)
        {
            transform.position += direction * (speed * Time.deltaTime);
        }
        if (destroyTimer > 2)
        {
            DestroyObject(this.gameObject);
        }
    }

    public override void FireBullet(GameObject launcher, GameObject target)
    {
        if (launcher && target)
        {
            direction = (target.transform.position - launcher.transform.position).normalized;
            fired = true;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == attackTag)
        {
            Destroy(this.gameObject);
        }
        if (col.collider.gameObject.tag == turretTag)
        {
            Destroy(this.gameObject);
        }
        if (col.collider.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (col.collider.gameObject.tag == "turret")
        {
            if (col.collider.gameObject.GetComponentInChildren<TowerScript>())
            {
                col.collider.gameObject.GetComponentInChildren<TowerScript>().hp -= 1;
            }
            else if (col.collider.gameObject.GetComponentInChildren<ExplosiveTowerScript>())
            {
                col.collider.gameObject.GetComponentInChildren<ExplosiveTowerScript>().hp -= 1;
            }
            else if (col.collider.gameObject.GetComponentInChildren<PoisonTowerScript>())
            {
                col.collider.gameObject.GetComponentInChildren<PoisonTowerScript>().hp -= 1;
            }
            Destroy(this.gameObject);
        }
    }
}

