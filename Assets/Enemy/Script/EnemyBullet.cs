using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BaseBullet
{

    Vector3 direction;
    bool fired;
    float destroyTimer;
    public float damage;
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
            Destroy(this.gameObject);
        }
    }
}

