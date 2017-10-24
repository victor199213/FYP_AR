using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "PowerUp")
        {
            if(GetComponentInChildren<TowerScript>())
            {
                if(GetComponentInChildren<TowerScript>().hp <= 0)
                {
                    return;
                }
            }
            else if(GetComponentInChildren<ExplosiveTowerScript>())
            {
                if (GetComponentInChildren<ExplosiveTowerScript>().hp <= 0)
                {
                    return;
                }
            }
            else if(GetComponentInChildren<PoisonTowerScript>())
            {
                if (GetComponentInChildren<PoisonTowerScript>().hp <= 0)
                {
                    return;
                }
            }


            int power = collision.collider.gameObject.GetComponent<PowerUp>().powerUpType;
            if (power == 1)
            {
                GetComponentInChildren<Shooting>().fireRate -= 0.1f;
                GetComponentInChildren<Shooting>().standardFireRate -= 0.1f;
            }
            else if (power == 2)
            {
                GetComponentInChildren<Shooting>().detectionDistance += 2;
            }
            else if(GetComponentInChildren<TowerScript>())
            {
                switch(power)
                {
                    case 3:
                        GetComponentInChildren<TowerScript>().hp += 5;
                        GetComponentInChildren<TowerScript>().maxHP += 5;
                        break;
                    case 4:
                        GetComponentInChildren<TowerScript>().damage += 1;
                        GetComponentInChildren<TowerScript>().standardDamage += 1;
                        break;
                }
            }
            else if (GetComponentInChildren<ExplosiveTowerScript>())
            {
                switch (power)
                {
                    case 3:
                        GetComponentInChildren<ExplosiveTowerScript>().hp += 5;
                        GetComponentInChildren<ExplosiveTowerScript>().maxHP += 5;
                        break;                 
                    case 4:                    
                        GetComponentInChildren<ExplosiveTowerScript>().damage += 1;
                        GetComponentInChildren<ExplosiveTowerScript>().standardDamage += 1;
                        break;
                }
            }
            else if (GetComponentInChildren<PoisonTowerScript>())
            {
                switch (power)
                {
                    case 3:
                        GetComponentInChildren<PoisonTowerScript>().hp += 5;
                        GetComponentInChildren<PoisonTowerScript>().maxHP += 5;
                        break;                 
                    case 4:                    
                        GetComponentInChildren<PoisonTowerScript>().damage += 1;
                        GetComponentInChildren<PoisonTowerScript>().standardDamage += 1;
                        break;
                }
            }
            Destroy(collision.collider.gameObject);
        }
    }
}
