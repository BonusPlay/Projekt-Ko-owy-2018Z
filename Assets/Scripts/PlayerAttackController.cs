using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour {

	/// <summary>
	/// For how long did the player hold the fire button, used to create more lethal bullets
	/// </summary>
	private float timeHeld;
	[SerializeField]
	private GameObject BulletPrefab;

	private Transform playerTR;
	public Vector3 BulletSpawnOffset = Vector3.zero;
	[SerializeField]
	private float bulletSpeed;

	[SerializeField]
	private int[] Hues;

	[SerializeField]
	private GameObject Display;
	private SpriteRenderer DisplaySR;
	private BulletDisplay DisplayBD;

	
	// Update is called once per frame

	void Start(){
		playerTR = this.gameObject.transform;

		DisplaySR = Display.GetComponent<SpriteRenderer>();
		DisplayBD = Display.GetComponent<BulletDisplay>();
	}
	void Update () {
		if(Input.GetAxis("Fire1") > 0.5f){
			timeHeld+=Time.deltaTime * 2f;
			if(timeHeld>0.1f){
				DisplaySR.enabled = true;
				DisplayBD.Hue = Hues[Mathf.FloorToInt(timeHeld)];
			}
			
			if(timeHeld > 5f){
				Fire();
			}
		}
		else{
			Fire();
			DisplaySR.enabled = false;
		}
		
	}

	private void Fire(){
		if(timeHeld>1f){
				var bullet = Instantiate(BulletPrefab, transform.position + BulletSpawnOffset, Quaternion.identity);
				var bullet_scr = bullet.GetComponent<PlayerProjectileScript>();
				bullet_scr.transformation = playerTR.localScale.x == 1? new Vector3(bulletSpeed,0,0): new Vector3(-bulletSpeed,0,0);
				bullet_scr.Damage = Mathf.FloorToInt(timeHeld);
			}
			timeHeld=0f;
	}
}
