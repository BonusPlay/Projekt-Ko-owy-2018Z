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
	}

	void Flip()
	{
		FacingRight = !FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}