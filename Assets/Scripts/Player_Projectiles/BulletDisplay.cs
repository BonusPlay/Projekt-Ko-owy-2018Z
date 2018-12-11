using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDisplay : MonoBehaviour {

	public float Hue;
	
	[SerializeField]
	private SpriteRenderer SR;

	[SerializeField]
	private Sprite[] sprites;
	
	// Update is called once per frame
	void Update () {
		SR.sprite = sprites[0];
		if(Hue == 317){
			SR.color = Color.HSVToRGB(0,0,30f/100f);
			return;
		}

		SR.color = Color.HSVToRGB(Hue/360f,1,1);
	}
}
