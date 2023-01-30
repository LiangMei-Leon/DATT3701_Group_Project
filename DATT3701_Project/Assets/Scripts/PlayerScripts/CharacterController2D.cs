using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 15f;							// Amount of force added when the player jumps.
	[SerializeField] private float runAcceleration = 0.5f;
	[SerializeField] private float runDecceleration = 0.5f;
	[SerializeField] private float runMaxSpeed = 3f;
	[HideInInspector] private float runAccelAmount;
	[HideInInspector] private float runDeccelAmount;

	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private float LastOnGroundTime;
	[SerializeField] private float LastPressedJumpTime;
	[SerializeField] private bool IsJumping;
	[SerializeField] public float coyoteTime = 0.2f;
	[SerializeField] public float jumpInputBufferTime = 0.2f;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

	}

	private void Update()
	{	
        LastOnGroundTime -= Time.deltaTime;
		LastPressedJumpTime -= Time.deltaTime;
		
		if(!IsJumping)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject  && !IsJumping)
				{
					LastOnGroundTime = coyoteTime;
				}
			} 
		}

		if (IsJumping && m_Rigidbody2D.velocity.y < 0)
		{
			IsJumping = false;
		}

		if (CanJump() && LastPressedJumpTime > 0)
		{
			IsJumping = true;
			Jump();
		}
    }

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			/*
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			*/
			//Calculate the direction we want to move in and our desired velocity
			float targetSpeed = move;
			float accelRate = 0f;
			//Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
			runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
			runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;
			
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
			
			//Calculate difference between current velocity and desired velocity
			float speedDif = targetSpeed - m_Rigidbody2D.velocity.x;
			//Calculate force along x-axis to apply to thr player

			float movement = speedDif * accelRate;

			//Convert this to a vector and apply to rigidbody
			m_Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
			Debug.Log("targetspeed" + targetSpeed);
			Debug.Log("accelRate" + accelRate);





			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (jump)
		{
			LastPressedJumpTime = jumpInputBufferTime;
		}
	}
	private void Jump()
    {
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		//We increase the force applied if we are falling
		//This means we'll always feel like we jump the same amount 
		//(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
		float force = m_JumpForce;
		if (m_Rigidbody2D.velocity.y < 0)
			force -= m_Rigidbody2D.velocity.y;

		m_Rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

	private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
	}
}
