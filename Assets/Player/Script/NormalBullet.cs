﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BaseBullet {

    Vector3 direction;
    bool fired;
    float destroyTimer;
    public string attackTag;

    void Update()
    {
        destroyTimer += Time.deltaTime;
        if(fired)
        {
            transform.position += direction * (speed * Time.deltaTime);
        }
        if(destroyTimer > 2)
        {
            DestroyObject(this.gameObject);
        }
    }

    public override void FireBullet(GameObject launcher, GameObject target)
    {
        if(launcher && target)
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
        if (col.collider.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
