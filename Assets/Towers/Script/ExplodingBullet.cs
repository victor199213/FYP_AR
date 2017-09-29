using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : BaseBullet {

    Vector3 direction;
    bool fired;
    float destroyTimer;
    public string attackTag;
    public float explosionDamage;

    private bool exploding;

    private void Start()
    {
        exploding = false;
    }

    void Update()
    {
        destroyTimer += Time.deltaTime;

        if(exploding == true)
        {
            this.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
        }

        if (fired && destroyTimer < 2)
        {
            transform.position += direction * (speed * Time.deltaTime);
        }
        else if (destroyTimer >= 2 && destroyTimer < 3 && exploding == false)
        {
            //DestroyObject(this.gameObject);
            explode();
        }

        if(destroyTimer >= 3)
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
        if (col.collider.gameObject.tag == attackTag && exploding == false)
        {
            //Destroy(this.gameObject);
            explode();
        }
        if (col.collider.gameObject.tag == "Wall" && exploding == false)
        {
            //Destroy(this.gameObject);
            explode();
        }
    }

    public void explode()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        exploding = true;
        destroyTimer = 2;
    }
}
