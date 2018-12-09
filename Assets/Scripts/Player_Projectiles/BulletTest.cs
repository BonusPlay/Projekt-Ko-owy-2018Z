using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour {

	public void TakeDamage(int damage){
		this.gameObject.name = ("took damage: " + damage);
		transform.position = transform.position + (Vector3.up * damage);
	}
}
