using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 18f;							// Amount of force added when the player jumps.
	[SerializeField] private float runAcceleration = 1f;
	[SerializeField] private float runDecceleration = 2f;
	[SerializeField] private float runMaxSpeed = 5f;
	[HideInInspector] private float runAccelAmount;
	[HideInInspector] private float runDeccelAmount;

	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private LayerMask m_Box;
	[SerializeField] private LayerMask m_NormalGround;							
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private float LastOnGroundTime;
	[SerializeField] private float LastPressedJumpTime;
	[SerializeField] public bool IsJumping;
	[SerializeField] public float coyoteTime = 0.1f;
	[SerializeField] public float jumpInputBufferTime = 0.1f;
	private SpriteRenderer playerSprite;
	public bool fliped = false;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public bool jumpable = false;
	public bool readyToFall = false;

	[Header("VFX")]
	public float interval = 1f;
	private float vfxTimer1;
	private ParticleSystem movingVFX;
	private ParticleSystem landingVFX;

	private AudioManager audioManager;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		movingVFX = this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
		landingVFX = this.gameObject.transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>();
		playerSprite = gameObject.GetComponent<SpriteRenderer>();

		audioManager = FindObjectOfType<AudioManager>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

	}

	private void Update()
	{	
        LastOnGroundTime -= Time.deltaTime;
		LastPressedJumpTime -= Time.deltaTime;
		jumpable = CanJump();
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

		if(m_Rigidbody2D.velocity.y < -5f)
		{
			readyToFall = true;
		}

		if(readyToFall)
		{
			Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_NormalGround);
			for (int i = 0; i < colliders2.Length; i++)
			{
				if (colliders2[i].gameObject != gameObject  && !IsJumping && !audioManager.checkIsPlaying("LemonWalking") && !audioManager.checkIsPlaying("LemonWalking02")&& !audioManager.checkIsPlaying("LemonWalking03"))
				{
					landingVFX.Play();
					float random = Random.Range(-6f,6f);
					if(random >= -6f && random < -2f)
						audioManager.Play("LemonWalking");
					else if(random >= -2f && random < 2f)
						audioManager.Play("LemonWalking02");
					else
						audioManager.Play("LemonWalking03");
					readyToFall = false;
				}
			}
			Collider2D[] colliders3 = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_Box);
			for (int i = 0; i < colliders3.Length; i++)
			{
				if (colliders3[i].gameObject != gameObject  && !IsJumping && !audioManager.checkIsPlaying("BoxStep01") && !audioManager.checkIsPlaying("BoxStep02") && !audioManager.checkIsPlaying("BoxStep03"))
				{
					landingVFX.Play();
					float random = Random.Range(-6f,6f);
					if(random >= -6f && random < -2f)
						audioManager.Play("BoxStep01");
					else if(random >= -2f && random < 2f)
						audioManager.Play("BoxStep02");
					else
						audioManager.Play("BoxStep03");
					readyToFall = false;
				}
			} 
		}
        if (ispengdao)
        {
			Timing();
		}
		

	}
	public bool ispengdao = false;

	public void Move(float move, bool jump)
	{
		if(LastOnGroundTime >= 0 && move != 0f)
		{
			vfxTimer1 -= Time.deltaTime;
			if(vfxTimer1 <= 0)
			{
				//movingVFX.Play();
				vfxTimer1 = interval;
			}
		}

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_NormalGround);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject  && !IsJumping && !audioManager.checkIsPlaying("LemonWalking") && !audioManager.checkIsPlaying("LemonWalking02") && !audioManager.checkIsPlaying("LemonWalking03"))
				{
					if(LastOnGroundTime >= 0 && move != 0f)
					{
						//audioManager.randomVolumeAndPitch("LemonWalking");
						float random = Random.Range(-6f,6f);
						if(random >= -6f && random < -2f)
							audioManager.Play("LemonWalking");
						else if(random >= -2f && random < 2f)
							audioManager.Play("LemonWalking02");
						else
							audioManager.Play("LemonWalking03");
					}
				}
			}
		
		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_Box);
			for (int i = 0; i < colliders2.Length; i++)
			{
				if (colliders2[i].gameObject != gameObject  && !IsJumping && !audioManager.checkIsPlaying("BoxStep01") && !audioManager.checkIsPlaying("BoxStep02") && !audioManager.checkIsPlaying("BoxStep03"))
				{
					if(LastOnGroundTime >= 0 && move != 0f)
					{
						//audioManager.randomVolumeAndPitch("LemonWalking");
						float random = Random.Range(-6f,6f);
						if(random >= -6f && random < -2f)
							audioManager.Play("BoxStep01");
						else if(random >= -2f && random < 2f)
							audioManager.Play("BoxStep02");
						else
							audioManager.Play("BoxStep03");
					}
				}
			}


		//only control the player if grounded or airControl is turned on 
		if (m_AirControl)
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
			readyToFall = true;
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
		m_FacingRight = !m_FacingRight;
		fliped = !fliped;
		if(playerSprite.flipX == false)
		    playerSprite.flipX = true;
        else
            playerSprite.flipX = false;
		
	}

	private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
	}
	public GameObject 提示, 设置;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="开始")
        {
			ispengdao = true;
			a = "开始";
		}
		if (collision.gameObject.name == "退出")
		{
			ispengdao = true;
			a = "退出";
		}

		if (collision.gameObject.name == "credits")
		{
			提示.SetActive(true);
		}
		if (collision.gameObject.name == "setting")
		{
			设置.SetActive(true);
		}
	}

    public void OnTriggerExit2D(Collider2D collision)
    {

		if (collision.gameObject.name == "credits")
		{
			提示.SetActive(false);
		}
		if (collision.gameObject.name == "setting")
		{
			设置.SetActive(false);
		}
	}

    public string a = "";
	public float secound = 5;
	private float nextTime = 1;
	public Text txtTimer;
	private void Timing()
	{
		if (secound <= 0)
		{
			Debug.Log(a);
			if (a== "开始")
            {
				SceneManager.LoadScene("2");
			}
			if (a == "退出")
			{
				Application.Quit();
			}
			return;
		}
		
		if (Time.time >= nextTime)
		{
			secound -= 1;
			Debug.Log(secound);
			nextTime = Time.time + 1;
			txtTimer.text=""+ secound;
		}
	}
}
