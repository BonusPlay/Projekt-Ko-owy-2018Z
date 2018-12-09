using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementController : MonoBehaviour
{
	[HideInInspector]
	public bool FacingRight = true;

	[HideInInspector]
	public bool Jump = false;

	public float MoveForce = 365f;
	public float MaxSpeed = 5f;
	public float JumpForce = 1000f;

	#region Anti Slide Variables and Properties

	[Tooltip("divide player's speed by this amount every time fixed update is being called, only if player is not generating input on horizontal axis, and IsOnIce = false")]
	[Range(1.1f,50f)]
	/// <summary>
	/// divide player's speed by this amount every time fixed update is being called, only if player is not generating input on horizontal axis, and IsOnIce = false, and grounded = true;
	/// </summary>
	public float AntiSlideStrength = 10f;
	[Tooltip("false = apply anti Slide, true = let the player slide")]	
	/// <summary>
	/// false = apply anti Slide, false = let the player slide
	/// </summary>
	public bool IsOnIce = false;

	#endregion

	public LayerMask GroundLayer;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;
	

	// Use this for initialization
	void Awake()
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
#if DEBUG
		Debug.DrawRay(rb2d.position, Vector2.down, Color.green);
#endif
		grounded = Physics2D.Raycast(rb2d.position, Vector2.down, 0.61f, GroundLayer).collider != null;

		if (Input.GetButtonDown("Jump") && grounded)
			Jump = true;
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");

		//anim.SetFloat("Speed", Mathf.Abs(h));

		if (horizontal * rb2d.velocity.x < MaxSpeed)
			rb2d.AddForce(Vector2.right * horizontal * MoveForce);

		if (Mathf.Abs(rb2d.velocity.x) > MaxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * MaxSpeed, rb2d.velocity.y);

		// Flip sprite on direction change
		if (horizontal > 0 && !FacingRight)
			Flip();
		else if (horizontal < 0 && FacingRight)
			Flip();

		if (Jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, JumpForce));
			Jump = false;
		}

		//stop the player from sliding
		AntiSlide(horizontal);
	}

	void Flip()
	{
		FacingRight = !FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	#region Anti Sliding Functionality


		private void AntiSlide(float inputAxis){

			float antiSlide = 2f;
			if(AntiSlideStrength >= 1f){
				antiSlide = Mathf.Min(AntiSlideStrength,50f);
			}
			

			//if the player is not trying to move, and is not on ice
			if(Mathf.Abs(inputAxis) < 0.3f && !IsOnIce && grounded){
				//if the players speed is higher than 0.1f (to not divide player's speed forever)
				if( Mathf.Abs(rb2d.velocity.x) > 0.1f){
					rb2d.velocity = new Vector2(rb2d.velocity.x /antiSlide, rb2d.velocity.y);
				}
				else{
					rb2d.velocity = new Vector2(0, rb2d.velocity.y);
				}				
			}

		}
	#endregion
}