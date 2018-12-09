using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour {


	public float Hue;
	public Sprite[] Sprites;
	int currentSprite = 0;
	float timer = 0;
	public float AnimationRate = 0.2f;
	SpriteRenderer SR;

	[SerializeField]
	private ParticleSystem PS;
	// Use this for initialization
	void Start () {
		SR = this.gameObject.GetComponent<SpriteRenderer>();
		ForceColorRefresh();
	}
	
	// Update is called once per frame
	void Update () {
		
		SR.sprite = Sprites[currentSprite];
		timer+=Time.deltaTime;
		if(timer>AnimationRate){
			timer=0;
			currentSprite = (currentSprite+1) == Sprites.Length? 0:currentSprite+1;		
		}
	}

	public void ForceColorRefresh(){
		SR.color = Color.HSVToRGB(Hue/360,1,1);
		var temp = PS.trails; 
		temp.colorOverTrail = Color.HSVToRGB(Hue/360,1,1);
		
	}

	
}
