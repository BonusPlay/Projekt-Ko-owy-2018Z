using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cinemachine.CinemachineVirtualCamera))]
public class CameraFollowPlayerScript : MonoBehaviour {

	[SerializeField]
	[Tooltip("The cinemachine camera script you want me to controll, is't probably the script above me.")]
	private Cinemachine.CinemachineVirtualCamera cinemachineCamera;


	void Start () {
		//no camera to controll, fix it
		if(cinemachineCamera==null){
			Debug.LogWarning("Camera was not specified, attempting to find it.");
			cinemachineCamera = this.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();			
		}
		//camera has no follow target, attempt to find player
		if(cinemachineCamera.Follow == null){
			Debug.LogWarning("Follow target was not specified, attempting to find GameObject with 'Player' Tag.");
			cinemachineCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
	
}
