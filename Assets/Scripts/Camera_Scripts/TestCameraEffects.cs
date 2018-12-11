using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraEffects : MonoBehaviour {

	private CameraEffects ce;

	void Start(){
		ce = this.gameObject.GetComponent<CameraEffects>();
	}

	/// <summary>
	/// only for testing inside unity's editor
	/// </summary>
	public void TestZoom(){
		ce.Zoom(2f,2f,0.1f,0);
	}
	/// <summary>
	/// only for testing inside unity's editor
	/// </summary>
	public void TestLookAt(){

		var test = GameObject.FindObjectsOfType<BoxCollider2D>();

		foreach (var item in test)
		{
			if(item.gameObject.name == "LookAtTestCube"){
				ce.QuickLookAt(item.transform,2);
			}
		}

	}

	public void TestScreenShake(){
		ce.ScreenShake(0.2f,2f);
	}
}
