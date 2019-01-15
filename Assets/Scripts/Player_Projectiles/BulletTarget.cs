using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour {

	public Component hitTarget;

	public void Hit(GameObject other, int damage){
        other.GetComponent<PlayerProjectileScript>().DestroyBullet();
        hitTarget.SendMessage("TakeDamage", damage);
		
	}

}
