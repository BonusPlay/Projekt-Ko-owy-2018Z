using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour {

	public Component hitTarget;

	public void Hit(GameObject other, int damage){
		hitTarget.SendMessage("TakeDamage", damage);
		other.GetComponent<PlayerProjectileScript>().DestroyBullet();
	}

}
