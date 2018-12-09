using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour {


	public int[] Hues;
	private float life = 5f;
	public int Damage;
	public Vector3 transformation;
	private DiscoBall DB;

	[SerializeField]
	private GameObject DeathParticles;

	void Start(){
		DB = this.gameObject.GetComponent<DiscoBall>();
		DB.Hue = Hues[Damage-1];
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (transformation *Time.deltaTime);
		life-=Time.deltaTime;
		if(life<0){
			Destroy(this.gameObject);
		}
	}

	public void DestroyBullet(){
		var particles = Instantiate(DeathParticles,transform.position,Quaternion.identity);
		var main = particles.GetComponent<ParticleSystem>().main;
		main.startColor = Color.HSVToRGB(DB.Hue/360,1,1);
		Destroy(this.gameObject);
		this.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<BulletTarget>() != null){
			other.GetComponent<BulletTarget>().Hit(this.gameObject, Damage);
		}
	}
}
