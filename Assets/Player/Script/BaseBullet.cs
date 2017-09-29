using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    public float speed = 5.0f;
    public abstract void FireBullet(GameObject launcher, GameObject target);

}
