using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : BaseBullet {

    Vector3 direction;
    bool fired;
    float destroyTimer;
    public string attackTag;

    private void Start()
    {
        this.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
    }

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
            
            if(col.gameObject.name == "Enemy(Clone)")
            {
                col.gameObject.GetComponent<Enemy>().poisonHit();
            }
            else if (col.gameObject.name == "EnemyRange(Clone)")
            {
                col.gameObject.GetComponent<EnemyRange>().poisonHit();
            }
            col.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
            Destroy(this.gameObject);
        }
        if (col.collider.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
