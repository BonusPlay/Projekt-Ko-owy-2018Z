using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEffect {

	// Use this for initialization
	public float time;
	public float strength;

	public CameraShakeEffect(float Time, float Strength){
		this.time = Time;
		this.strength = Strength;
	}

	
	public void RemoveTime(float ammount){
		this.time -= ammount;
	}
}
