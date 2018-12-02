using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomEffect {

	public float time;
	public float strength;

	public float smoothing;
	public int priority;

	public CameraZoomEffect(float Time, float Strength, float Smoothing,int Priority){
		this.time = Time;
		this.strength = Strength;
		this.priority = Priority;
		this.smoothing = Smoothing;
	}

	
	public void RemoveTime(float ammount){
		this.time -= ammount;
	}

}
