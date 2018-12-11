using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cinemachine.CinemachineVirtualCamera))]

public class CameraEffects : MonoBehaviour {

	[SerializeField]
	[Tooltip("The cinemachine camera script you want me to controll")]
	private Cinemachine.CinemachineVirtualCamera cinemachineCamera;

#region ZoomVariables

	private float zoomWithoutEffects;

	/// <summary>
	/// use this to change camera's orthographic size permanently
	/// </summary>
	public float DefaultZoom{
		set{
			zoomWithoutEffects = value;
		}
		get{
			return zoomWithoutEffects;
		}
	}
	private CameraZoomEffect currentZoomEffect = null;
	
#endregion


	#region ScreenShakeVariables 
	private CameraShakeEffect currentShakeEffect = null;

#endregion
	
#region QuickLookAtVariables
	private Transform oldFollowTarget;
	private Transform QuickLookTarget;

	private Transform followTarget{
		get{
			if(cinemachineCamera.Follow != oldFollowTarget && cinemachineCamera.Follow != QuickLookTarget){
				oldFollowTarget = cinemachineCamera.Follow;
			}
			return oldFollowTarget;
		}
	}

	private float lookAtTime;
		
	#endregion
	void Start () {
		//no camera to controll, fix it
		if(cinemachineCamera==null){
			Debug.LogWarning("Camera was not specified, attempting to find any.");
			cinemachineCamera = GameObject.FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();	
			if(cinemachineCamera == null){
				Debug.LogError("No camera to controll, effects are disabled!");
				this.enabled = false;
				return;
			}		
		}
		zoomWithoutEffects = cinemachineCamera.m_Lens.OrthographicSize;
		oldFollowTarget = cinemachineCamera.Follow;
	}

    void Update()
    {
        applyZoom();
		applyShake();
		LookAt();
    }

	/// <summary>
	/// Quick zoom effect
	/// </summary>
	/// <param name="time">Duration of effect in seconds</param>
	/// <param name="zoomValue">Orthographic size you want the camera to reach, lower = zoomed in, higher = zoomed out</param>
	/// <param name="smoothing">Amount of smoothing you want in transition, 1 or 0 = no smoothing, 0.5 = fast smoothing, 0.1 = slow smoothing,</param>
	/// <param name="priority">In case of multiple zoom effect added at once, only the highest priority effect is applied</param>
	public void Zoom(float time =2f, float zoomValue = 3f, float smoothing =0.1f, int priority =0){
		if(smoothing <= 0f){
			smoothing = 1f;
		}
		if(currentZoomEffect == null){
			currentZoomEffect = new CameraZoomEffect(time,zoomValue,smoothing,priority);
		}	
		else{
			if(currentZoomEffect.priority < priority){
				currentZoomEffect = new CameraZoomEffect(time,zoomValue,smoothing,priority);
			}
		}
	}

	private void applyZoom(){
		if (currentZoomEffect != null)
        {
            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.m_Lens.OrthographicSize, currentZoomEffect.strength, currentZoomEffect.smoothing);
            currentZoomEffect.RemoveTime(Time.deltaTime);
            if (currentZoomEffect.time < 0)
            {
                currentZoomEffect = null;
            }
        }
        else if(cinemachineCamera.m_Lens.OrthographicSize != zoomWithoutEffects)
        {
            cinemachineCamera.m_Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.m_Lens.OrthographicSize, zoomWithoutEffects, 0.1f);
            if (Mathf.Abs(cinemachineCamera.m_Lens.OrthographicSize - zoomWithoutEffects) < 0.1f)
            {
                cinemachineCamera.m_Lens.OrthographicSize = zoomWithoutEffects;
            }
        }
	}


	/// <summary>
	/// Quick screen shake effect
	/// </summary>
	/// <param name="time">duration in seconds</param>
	/// <param name="strength">strength, recomended value = 2</param>
	public void ScreenShake(float time = 0.1f, float strength = 2){
		strength = Mathf.Abs(strength);
		if(currentShakeEffect == null){
			currentShakeEffect = new CameraShakeEffect(time,strength);
		}	
		else{
			if(currentShakeEffect.strength < strength){
				currentShakeEffect = new CameraShakeEffect(time,strength);
			}
		}
	}

	/// <summary>
	/// make the deadzone = 0, remove damping, shake the screen. Return to normal values afterwards
	/// </summary>
	private void applyShake(){
		if (currentShakeEffect != null)
		{
			if(currentShakeEffect.time > 0){
				currentShakeEffect.time -= Time.deltaTime;

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX += Random.Range((-currentShakeEffect.strength)*Time.deltaTime,(currentShakeEffect.strength)*Time.deltaTime);

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY += Random.Range((-currentShakeEffect.strength)*Time.deltaTime,(currentShakeEffect.strength)*Time.deltaTime);

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth=0;

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight=0;

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_XDamping = 0;

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_YDamping = 0;

				cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ZDamping = 0;
			}
			else{
				currentShakeEffect = null;
			}
		}
		else{
			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX = 0.5f;
			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = 0.5f;

			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneHeight=0.04f;
			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_DeadZoneWidth=0.2f;

			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_XDamping = 1;
			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_YDamping = 1;
			cinemachineCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ZDamping = 1;
		}
	}
	

	/// <summary>
	/// Make the camera look at something for a brief moment, such as an exit or dangerous enemy, then return to it's old target
	/// </summary>
	/// <param name="target">target's transform</param>
	/// <param name="time">duration in seconds</param>
	public void QuickLookAt(Transform target, float time =2f){
		QuickLookTarget = target;
		lookAtTime = time;
	}

	private void LookAt(){

		if(oldFollowTarget != followTarget){
			if(QuickLookTarget != null && lookAtTime > 0){
				cinemachineCamera.Follow = QuickLookTarget;
				lookAtTime-=Time.deltaTime;
			}
			else{
				cinemachineCamera.Follow = followTarget;
			}
			return;
		}
		if (lookAtTime > 0)
		{		
			lookAtTime-=Time.deltaTime;
			cinemachineCamera.Follow = QuickLookTarget;
		}
		else{
			cinemachineCamera.Follow = followTarget;
		}
		
		
	}

	
}

