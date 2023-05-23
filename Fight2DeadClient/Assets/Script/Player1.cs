using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    private Animator m_animator;
	private Rigidbody2D m_body2d;
    private Sensor m_groundSensor;
    private bool m_grounded = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    //DOUBLE JUMP
    private int m_jumpsLeft = 2;
    private bool canDoubleJump;
    //FIGHT1
    public Transform attackPoint;
    public float attackRangeX;
    public float attackRangeY;
    // public float attackoffset;
    [SerializeField] private float attackOffsetX;
    [SerializeField] private float attackOffsetY;

    public LayerMask enemyLayers;
    private bool isAttacking = false;
    //Knockback
    [SerializeField]
    public float knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    private bool applyKnockback;
    [SerializeField]
    private GameObject hitParticle;

    private float knockbackStart;

    private int playerFacingDirection;
    private int mylayerFacingDirection;
    private bool playerOnLeft, knockback;
    //Ultimate cooldown
    public float cooldownTime = 10f;
    private float lastUltimateTime = 0f;
    //Spawn
    public GameObject gameObject;
    public Vector3 spawnPosition;
    public int numberRespawn = 1;
    //test
    private string currentState;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_INTRO = "Intro";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_HURT_LEFT = "Hurt_Left";
    const string PLAYER_HURT_RIGHT = "Hurt_Right";
    const string PLAYER_DIE_BOTTOM = "Die_Bottom";
    const string PLAYER_DIE_LEFT = "Die_Left";
    const string PLAYER_ATTACK = "Nor";
    public float animTime;

    private GameState globalGameState = GameState.Instance;


	void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        m_animator.Play(newState);
        currentState = newState;
    }

    public int GetFacingDirection()
    {
        return m_facingDirection;
    }

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor>();
        // m_animator.SetTrigger("intro");
        ChangeAnimationState(PLAYER_INTRO);

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: just change animation using this way 
        if(globalGameState.player1IsBeingControlled)
		{
            Debug.Log("player 1 is being controlled with state: " + globalGameState.player1State);
            if(globalGameState.player1State == -1)
			{
                ChangeAnimationState(PLAYER_RUN);
			} else if(globalGameState.player1State == 1) 
			{
                //ChangeAnimationState(PLAYER_IDLE);
                ChangeAnimationState(PLAYER_RUN);
                Debug.Log("TODO: some how make the player move right  ");
			} else
			{
                ChangeAnimationState(PLAYER_IDLE);
			}
		}
        else
		{
			CheckKnockback();

			if (m_grounded)
			{
				canDoubleJump = true;
				m_jumpsLeft = 2; // reset jumps when grounded
			}
			else if (m_jumpsLeft == 2)
			{
				canDoubleJump = true;
			}
			else
			{
				canDoubleJump = false;
			}
			// Increase timer that controls attack combo
			m_timeSinceAttack += Time.deltaTime;


			//Check if character just landed on the ground
			if (!m_grounded && m_groundSensor.State())
			{
				m_grounded = true;
				// m_animator.SetBool("Grounded", m_grounded);
			}

			//Check if character just started falling
			if (m_grounded && !m_groundSensor.State() && m_body2d.velocity.y < 0)
			{

				m_grounded = false;
				// m_animator.SetBool("Grounded", m_grounded);
			}
			if (!m_grounded && !m_groundSensor.State() && m_body2d.velocity.y < 0)
			{

				// m_grounded = false;
				ChangeAnimationState(PLAYER_FALL);
				//Debug.Log("Gaara Fall");
				// m_animator.SetBool("Grounded", m_grounded);
			}
			// -- Handle input and movement --
			float inputX = 0f;
			/*
			if(isBeingControlled)
			{
				if (moveLeft)
				{
					inputX = -1f;
					GetComponent<SpriteRenderer>().flipX = true;
					m_facingDirection = -1;
				}

				else if (moveRight)
				{
					inputX = 1f;
					GetComponent<SpriteRenderer>().flipX = false;
					m_facingDirection = 1;
				}
			} else
			{
				if (Input.GetKey(KeyCode.A))
				{
					inputX = -1f;
					GetComponent<SpriteRenderer>().flipX = true;
					m_facingDirection = -1;
				}

				else if (Input.GetKey(KeyCode.D))
				{
					inputX = 1f;
					GetComponent<SpriteRenderer>().flipX = false;
					m_facingDirection = 1;
				}
			}
			*/

            // @Test
			if (Input.GetKey(KeyCode.A))
			{
				inputX = -1f;
				GetComponent<SpriteRenderer>().flipX = true;
				m_facingDirection = -1;
                globalGameState.player1State = -1; 
			} else
			{
                globalGameState.player1State = 0; 
			}

			if (Input.GetKey(KeyCode.D))
			{
				inputX = 1f;
				GetComponent<SpriteRenderer>().flipX = false;
				m_facingDirection = 1;
                globalGameState.player1State = 1; 
			} else
			{
                globalGameState.player1State = 0; 

			}


			//Set AirSpeed in animator
			// m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

			// -- Handle Animations --

			// //Hurt
			// if (Input.GetKeyDown("q"))
			//     m_animator.SetTrigger("Hurt");

			// Move
			if (!isAttacking && !knockback)
			{
				m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
                //Debug.Log("Gaara Speed");
			}

			IEnumerator PerformAttack()
			{
				m_body2d.velocity = Vector2.zero;
				// Call one of three attack animations "Attack1", "Attack2", "Attack3"
				ChangeAnimationState(PLAYER_ATTACK + m_currentAttack.ToString());
				//When the attack animation run the event in animation call the function Attack()
				//Debug.Log("Gaara Attack");

				// Wai until the animation attack end
				yield return new WaitForSeconds(animTime);

				// Reset isAttacking = false
				isAttacking = false;

				// Reset timer
				m_timeSinceAttack = 0.0f;
			}
			//Attack
			if (Input.GetKeyDown(KeyCode.J) && m_timeSinceAttack > 0.25f)
			{
				m_currentAttack++;
				// Loop back to one after third attack
				if (m_currentAttack > 3)
					m_currentAttack = 1;

				// Reset Attack combo if time since last attack is too large
				if (m_timeSinceAttack > 0.5f)
					m_currentAttack = 1;

				isAttacking = true;

				// Perform attack and wait for the attack animation to finish
				StartCoroutine(PerformAttack());
			}
			else if (Input.GetKeyDown(KeyCode.L) && (Time.time - lastUltimateTime >= cooldownTime))
			{
				// m_animator.SetTrigger("ultimate");
				//Debug.Log("Gaara Ultimate");
				lastUltimateTime = Time.time; // Update time last pressed "L" button
			}
			// Block
			// else if (Input.GetKeyDown(KeyCode.K))
			// {
			//     isBlocked = true;
			//     m_animator.SetTrigger("Block");
			//     m_animator.SetBool("IdleBlock", true);
			// }

			// else if (Input.GetKeyUp(KeyCode.K))
			// {
			//     isBlocked = false;
			//     m_animator.SetBool("IdleBlock", false);
			// }
			else if (Input.GetKeyDown("w"))
			{
				if (m_grounded)
				{
					// m_animator.SetTrigger("Jump");
					ChangeAnimationState(PLAYER_JUMP);
					//Debug.Log("Gaara Jump 1");

					m_grounded = false;
					// m_animator.SetBool("Grounded", m_grounded);
					m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
					m_groundSensor.Disable(0.2f);
				}
				else if (canDoubleJump)
				{
					// m_animator.SetTrigger("Jump");
					ChangeAnimationState(PLAYER_JUMP);
					//Debug.Log("Gaara Jump 2");
					m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
					m_jumpsLeft--;
				}
			}
			//Run
			else if (Mathf.Abs(inputX) > Mathf.Epsilon && m_grounded)
			{
				// Reset timer
				//Debug.Log("Gaara Walk");
				m_delayToIdle = 0.05f;
				// m_animator.SetInteger("AnimState", 1);
				ChangeAnimationState(PLAYER_RUN);
			}

			//Idle
			else if (m_grounded && !isAttacking)
			{
				//Debug.Log("Gaara Idle");
				// Prevents flickering transitions to idle
				m_delayToIdle -= Time.deltaTime;
				// if (m_delayToIdle < 0)
				// m_animator.SetInteger("AnimState", 0);
				if (m_delayToIdle < 0)
					ChangeAnimationState(PLAYER_IDLE);
			}
		}
    }

    private void Attack()
    {
        Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f); // Hướng của nhân vật
        Vector2 attackPointPosition = (Vector2)transform.position + new Vector2(attackOffsetX * attackDirection.x, attackOffsetY); // Tính toán vị trí của attackPoint
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointPosition, attackRange, enemyLayers);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPointPosition, new Vector2(attackRangeX, attackRangeY), 0f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Player2 hurtComponent = enemy.GetComponent<Player2>();
            if (hurtComponent != null)
            {
                //Debug.Log("Gaara Attack");
                hurtComponent.Damage(m_facingDirection);
            }
            else
            {
                Debug.LogError("Player2 component is null");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f);
        Vector2 attackPointPosition = (Vector2)transform.position + new Vector2(attackOffsetX * attackDirection.x, attackOffsetY);
        // Gizmos.DrawWireSphere(attackPointPosition, attackRange);
        Gizmos.DrawWireCube(attackPointPosition, new Vector2(attackRangeX, attackRangeY));
    }

    //Emotional Damage
    public bool checkKnock()
    {
        return knockback;
    }

    public void Damage(int playerFacingDirection)
    {
        Instantiate(hitParticle, m_animator.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1 && m_facingDirection == 1 || playerFacingDirection != 1 && m_facingDirection != 1)
        {
            playerOnLeft = true;
        }
        if (playerFacingDirection != 1 && m_facingDirection == 1 || playerFacingDirection == 1 && m_facingDirection != 1)
        {
            playerOnLeft = false;
        }


        // m_animator.SetBool("playerOnLeft", playerOnLeft);
        // m_animator.SetTrigger("damage");
        if (applyKnockback)
        {

            //Knockback
            Knockback(playerFacingDirection);
            knockbackSpeedX++;
        }

    }

    public void Knockback(int playerFacingDirection)
    {
        knockback = true;
        knockbackStart = Time.time;

        float horizontalForce = knockbackSpeedX * playerFacingDirection;

        StartCoroutine(KnockbackCurve(horizontalForce));
        if (playerOnLeft)
        {
            ChangeAnimationState(PLAYER_HURT_LEFT);
        }
        if(!playerOnLeft)
        {
            ChangeAnimationState(PLAYER_HURT_RIGHT);
        }
        Debug.Log("Gaara Hurt");
    }

    private IEnumerator KnockbackCurve(float horizontalForce)
    {
        float timeInAir = 0f;
        float maxTimeInAir = 0.5f; // Thời gian knockback lên trời
        float maxHeight = 2f; // Độ cao tối đa của knockback

        while (timeInAir < maxTimeInAir)
        {
            float t = timeInAir / maxTimeInAir;
            float yOffset = Mathf.Sin(t * Mathf.PI) * maxHeight;

            Vector2 knockbackForce = new Vector2(horizontalForce, knockbackSpeedY + yOffset);
            m_body2d.velocity = knockbackForce;

            timeInAir += Time.deltaTime;
            yield return null;
        }

        // Reset vận tốc về 0 sau khi knockback kết thúc
        m_body2d.velocity = Vector2.zero;
        ChangeAnimationState(PLAYER_IDLE);
    }

    public void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            m_body2d.velocity = new Vector2(0.0f, m_body2d.velocity.y);
        }
    }

    public void SpawnObject()
    {
        float delay = 0.6f;
        Invoke("MoveObjectToSpawnPosition", delay);
    }

    private void MoveObjectToSpawnPosition()
    {
        if (numberRespawn > 0)
        {
            StartCoroutine(KeepObjectAtSpawnPosition());
            numberRespawn--;
        }
        else
        {
            Debug.Log("Gaara actually dead");
            gameObject.SetActive(false);
        }
    }
    private IEnumerator KeepObjectAtSpawnPosition()
    {
        Debug.Log("Gaara Respawn");
        // Đặt vị trí nhân vật về spawnPosition
        gameObject.transform.position = spawnPosition;
        // Vô hiệu hóa trọng lực
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // Đợi 2 giây
        yield return new WaitForSeconds(2f);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;

    }
    public void Die()
    {

        // Play the die animation
        Debug.Log(gameObject.transform.position.y);
        if (gameObject.transform.position.y < -17f)
        {
            // m_animator.SetTrigger("die_bottom");
            ChangeAnimationState(PLAYER_DIE_BOTTOM);
        }
        else
        {
            // m_animator.SetTrigger("die_left");
            ChangeAnimationState(PLAYER_DIE_LEFT);
        }

        SpawnObject();
    }

    public void UpdateAttackOffsetX(float x)
    {
        attackOffsetX = x;
    }
    public void UpdateAttackOffsetY(float y)
    {
        attackOffsetY = y;
    }
    public void UpdateAttackRangeX(float x)
    {
        attackRangeX = x;
    }
    public void UpdateAttackRangeY(float y)
    {
        attackRangeY = y;
    }
    public void UpdateAnimTime(float a)
    {
        animTime = a;
    }

}
