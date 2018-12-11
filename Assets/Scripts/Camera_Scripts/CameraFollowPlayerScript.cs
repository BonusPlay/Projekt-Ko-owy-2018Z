using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cinemachine.CinemachineVirtualCamera))]
public class CameraFollowPlayerScript : MonoBehaviour {

	[SerializeField]
	[Tooltip("The cinemachine camera script you want me to controll, is't probably the script above me.")]
	private Cinemachine.CinemachineVirtualCamera cinemachineCamera;

	private GameObject player;

	/// <summary>
	/// currently saved player, might not be the current follow target!
	/// </summary>
	public GameObject GetSavedPlayer{
		get{
			return player;
		}
	}

	/// <summary>
	/// what the camera is looking at currently.
	/// </summary>
	/// <value></value>
	public Transform GetFollowTarget{
		get{
			return cinemachineCamera.Follow;
		}
	}

	void Start () {
		//no camera to controll, fix it
		if(cinemachineCamera==null){
			Debug.LogWarning("Camera was not specified, attempting to find it.");
			cinemachineCamera = this.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();			
		}
		//camera has no follow target, attempt to find player
		if(cinemachineCamera.Follow == null){
			Debug.LogWarning("Follow target was not specified, attempting to find GameObject with 'Player' Tag.");
			player = GameObject.FindGameObjectWithTag("Player");
			cinemachineCamera.Follow = player.transform;
		}
		else{
			player = cinemachineCamera.Follow.gameObject;
		}
	}

	/// <summary>
	/// Change saved player, useful when player's gameobject is destroyed and you want to follow a new one
	/// </summary>
	/// <param name="newPlayer">new player's gameobject</param>
	/// <param name="followNewPlayer">true = follow new player immediately, false = keep following current target</param>
	public void ChangeSavedPlayer(GameObject newPlayer, bool followNewPlayer){
		player = newPlayer;
		if(followNewPlayer){
			cinemachineCamera.Follow = player.transform;
		}
	}

	/// <summary>
	/// remove follow target
	/// </summary>
	public void StopFollowing(){
		cinemachineCamera.Follow = null;
	}

	/// <summary>
	/// assign saved player's transform as follow target
	/// </summary>
	public void StartFollowingPLayer(){
		cinemachineCamera.Follow = player.transform;
	}

	/// <summary>
	/// Change follow target, without changing the saved player. Useful when player controls other gameobject for unpecified amount of time. Use StartFollowingPLayer() to go back to player, use CamerEffects script QuickLookAt() when you want the camera to focus on something important for few seconds.
	/// </summary>
	/// <param name="target">new target's Transform</param>
	public void ChangeFollowTarget(Transform target){
		cinemachineCamera.Follow = target;
	}

}
