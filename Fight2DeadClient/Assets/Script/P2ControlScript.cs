using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SocketServer
{
	class P2ControlScript: MonoBehaviour
	{
		 float m_speed = 4.0f;
		 float m_jumpForce = 7.5f;
		private  Animator m_animator;
		private  Rigidbody2D m_body2d;
		private  Sensor m_groundSensor;
		private  bool m_grounded = false;
		private  int m_facingDirection = 1;
		private  int m_currentAttack = 0;
		private  float m_timeSinceAttack = 0.0f;
		private  float m_delayToIdle = 0.0f;
		//DOUBLE JUMP
		private  int m_jumpsLeft = 2;
		private  bool canDoubleJump;
		//FIGHT1
		public  Transform attackPoint;
		public  float attackRangeX;
		public  float attackRangeY;
		// public float attackoffset;
		[SerializeField] private  float attackOffsetX;
		[SerializeField] private  float attackOffsetY;

		public  LayerMask enemyLayers;
		private  bool isAttacking = false;
		//Knockback
		[SerializeField]
		public  float knockbackSpeedX, knockbackSpeedY, knockbackDuration;
		[SerializeField]
		private  bool applyKnockback;
		[SerializeField]
		private  GameObject hitParticle;

		private  float knockbackStart;

		// private  int m_facingDirection;
		private  int mylayerFacingDirection;
		private  bool playerOnLeft, knockback;
		//Ultimate cooldown
		public  float cooldownTime = 10f;
		private  float lastUltimateTime = 0f;

		//Spawn
		public  Vector3 spawnPosition;
		public  int numberRespawn = 1;
		private  bool isRespawn = false;
		private  string currentState;


		private  GameState globalGameState = GameState.Instance;
		const string PLAYER_ULTIMATE = "Ultimate";
		//time animation of attack
		public  float animTime;
		public  float nor1Time;
		public  float nor2Time;
		public  float nor3Time;
		public  float ultimateTime;

		//sound effect
		public  AudioSource attackSound;
		public  AudioSource attackkSound_1;
		public  AudioSource ultimateSound;
		public  AudioSource dieSound;

		//animation
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



		// enum
		const int WALK_LEFT = (-1);
		const int WALK_RIGHT = (1);
		const int JUMP = (2);
		const int FALL = (-2);
		const int IDLE = (0);
		const int HURT_LEFT = (-3);
		const int HURT_RIGHT = (-4);
		const int ATTACK1 = (3);
		const int ATTACK2 = (4);
		const int ATTACK3 = (5);
		const int ULTIMATE = (9);

		public Text textDame;
		public Text textRespawn;

		public  void ChangeAnimationState(string newState)
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
			initControlScript(globalGameState.p2CharName);
		}

		public void initControlScript(string charname)
		{
			GameObject c = GameObject.Find(charname);
			Player1 cs1 = c.GetComponent<Player1>();
			Player2 cs2 = c.GetComponent<Player2>();
			if(cs1 != null)
			{
				m_speed = cs1.m_speed;
				m_jumpForce = cs1.m_jumpForce;
				m_animator = cs1.m_animator;
				m_body2d = cs1.m_body2d;
				m_groundSensor = cs1.m_groundSensor;
				m_grounded = cs1.m_grounded;
				m_facingDirection = cs1.m_facingDirection;
				m_currentAttack = cs1.m_currentAttack;
				m_timeSinceAttack = cs1.m_timeSinceAttack;
				m_delayToIdle = cs1.m_delayToIdle;
				//DOUBLE JUMP
				m_jumpsLeft = cs1.m_jumpsLeft;
				canDoubleJump = cs1.canDoubleJump;
				//FIGHT1
				attackPoint = cs1.attackPoint;
				attackRangeX = cs1.attackRangeX;
				attackRangeY = cs1.attackRangeY;
				// public float attackoffset;
				attackOffsetX = cs1.attackOffsetX;
				attackOffsetY = cs1.attackOffsetY;

				enemyLayers = cs1.enemyLayers;
				isAttacking = false;

				knockbackSpeedX = cs1.knockbackSpeedX;
				knockbackSpeedY = cs1.knockbackSpeedY;
				knockbackDuration = cs1.knockbackDuration;
				applyKnockback = cs1.applyKnockback;
				hitParticle = cs1.hitParticle;

				knockbackStart = cs1.knockbackStart;

				m_facingDirection = cs1.m_facingDirection;
				mylayerFacingDirection = cs1.mylayerFacingDirection;
				playerOnLeft = cs1.playerOnLeft;
				knockback = cs1.knockback;

				cooldownTime = 10f;
				lastUltimateTime = 0f;

				spawnPosition = cs1.spawnPosition;
				numberRespawn = cs1.numberRespawn;
				isRespawn = cs1.isRespawn;
				currentState = cs1.currentState;

				attackSound = cs1.attackSound;
				attackSound = cs1.attackkSound_1;
				ultimateSound = cs1.ultimateSound;
				dieSound = cs1.dieSound;
				animTime = cs1.animTime;
				nor1Time = cs1.nor1Time;
				nor2Time = cs1.nor2Time;
				nor3Time = cs1.nor3Time;
				ultimateTime = cs1.ultimateTime;

				textDame = cs1.textDame;
				textRespawn = cs1.textRespawn;
			} else if(cs2 != null)
			{
				m_speed = cs2.m_speed;
				m_jumpForce = cs2.m_jumpForce;
				m_animator = cs2.m_animator;
				m_body2d = cs2.m_body2d;
				m_groundSensor = cs2.m_groundSensor;
				m_grounded = cs2.m_grounded;
				m_facingDirection = cs2.m_facingDirection;
				m_currentAttack = cs2.m_currentAttack;
				m_timeSinceAttack = cs2.m_timeSinceAttack;
				m_delayToIdle = cs2.m_delayToIdle;
				//DOUBLE JUMP
				m_jumpsLeft = cs2.m_jumpsLeft;
				canDoubleJump = cs2.canDoubleJump;
				//FIGHT1
				attackPoint = cs2.attackPoint;
				attackRangeX = cs2.attackRangeX;
				attackRangeY = cs2.attackRangeY;
				// public float attackoffset;
				attackOffsetX = cs2.attackOffsetX;
				attackOffsetY = cs2.attackOffsetY;

				enemyLayers = cs2.enemyLayers;
				isAttacking = false;

				knockbackSpeedX = cs2.knockbackSpeedX;
				knockbackSpeedY = cs2.knockbackSpeedY;
				knockbackDuration = cs2.knockbackDuration;
				applyKnockback = cs2.applyKnockback;
				hitParticle = cs2.hitParticle;

				knockbackStart = cs2.knockbackStart;

				m_facingDirection = cs2.m_facingDirection;
				mylayerFacingDirection = cs2.mylayerFacingDirection;
				playerOnLeft = cs2.playerOnLeft;
				knockback = cs2.knockback;

				cooldownTime = 10f;
				lastUltimateTime = 0f;

				spawnPosition = cs2.spawnPosition;
				numberRespawn = cs2.numberRespawn;
				isRespawn = cs2.isRespawn;
				currentState = cs2.currentState;

				attackSound = cs2.attackSound;
				attackSound = cs2.attackkSound_1;
				ultimateSound = cs2.ultimateSound;
				dieSound = cs2.dieSound;
				animTime = cs2.animTime;
				nor1Time = cs2.nor1Time;
				nor2Time = cs2.nor2Time;
				nor3Time = cs2.nor3Time;
				ultimateTime = cs2.ultimateTime;

				textDame = cs2.textDame;
				textRespawn = cs2.textRespawn;
			}

			ChangeAnimationState(PLAYER_INTRO);
		}

		// Update is called once per frame
		void Update()
		{
			UpdateText();
			float inputX = 0f;
			IEnumerator PerformUltimate(float animUltimateTime)
			{
				m_body2d.velocity = Vector2.zero;

				ChangeAnimationState(PLAYER_ULTIMATE);

				m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

				//When the attack animation run the event in animation call the function Attack()
				//Debug.Log("Gaara Ultimate");

				// Wai until the animation attack end
				yield return new WaitForSeconds(animUltimateTime);

				// Reset isAttacking = false
				isAttacking = false;

				// Update time last pressed "L" button
				lastUltimateTime = Time.time;
			}

			IEnumerator PerformAttack(string anim, float norTime)
			{
				m_body2d.velocity = Vector2.zero;

				ChangeAnimationState(anim);
				//When the attack animation run the event in animation call the function Attack()
				//Debug.Log("Gaara Attack");

				// Wai until the animation attack end
				yield return new WaitForSeconds(norTime);

				// Reset isAttacking = false
				isAttacking = false;
				// Reset timer
				m_timeSinceAttack = 0.0f;

			}

			// TODO: just change animation using this way 
			if (globalGameState.player2IsBeingControlled)
			{
				if (globalGameState.player2State == -1)
				{
					inputX = -1f;
					m_facingDirection = -1;
					GetComponent<SpriteRenderer>().flipX = true;
					ChangeAnimationState(PLAYER_RUN);
				}
				else if (globalGameState.player2State == 1)
				{
					inputX = 1f;
					m_facingDirection = 1;
					GetComponent<SpriteRenderer>().flipX = false;
					ChangeAnimationState(PLAYER_RUN);
				}
				else if (globalGameState.player2State == -2)
				{
					ChangeAnimationState(PLAYER_FALL);
				}
				else if (globalGameState.player2State == 2)
				{
					ChangeAnimationState(PLAYER_JUMP);
				}
				else if (globalGameState.player2State == ATTACK1)
				{
					attackSound.Play();
					StartCoroutine(PerformAttack((PLAYER_ATTACK + "1"), nor1Time));
				}
				else if (globalGameState.player2State == ATTACK2)
				{
					attackSound.Play();
					StartCoroutine(PerformAttack((PLAYER_ATTACK + "2"), nor2Time));
				}
				else if (globalGameState.player2State == ATTACK3)
				{
					attackkSound_1.Play();
					StartCoroutine(PerformAttack((PLAYER_ATTACK + "3"), nor3Time));
				}
				else if (globalGameState.player2State == ULTIMATE)
				{
					StartCoroutine(PerformUltimate(ultimateTime));
				}
				else if (globalGameState.player2State == HURT_LEFT)
				{
					ChangeAnimationState(PLAYER_HURT_LEFT);

				}
				else if (globalGameState.player2State == HURT_RIGHT)
				{

					ChangeAnimationState(PLAYER_HURT_RIGHT);
				}
				else
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
					globalGameState.player2State = -2;
					////Debug.Log("Gaara Fall");
					// m_animator.SetBool("Grounded", m_grounded);
				}
				// -- Handle input and movement --

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


				//Set AirSpeed in animator
				// m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

				// -- Handle Animations --

				// //Hurt
				// if (Input.GetKeyDown("q"))
				//     m_animator.SetTrigger("Hurt");

				// Move
				if (!isAttacking && !knockback && !isRespawn)
				{
					m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
					//Debug.Log("Gaara Speed");
				}

				// IEnumerator PerformAttack()
				// {
				//     m_body2d.velocity = Vector2.zero;
				//     // Call one of three attack animations "Attack1", "Attack2", "Attack3"
				//     ChangeAnimationState(PLAYER_ATTACK + m_currentAttack.ToString());
				//     //When the attack animation run the event in animation call the function Attack()
				//     //Debug.Log("Gaara Attack");

				//     // Wai until the animation attack end
				//     yield return new WaitForSeconds(0.3f);

				//     // Reset isAttacking = false
				//     isAttacking = false;
				//     // Reset timer
				//     m_timeSinceAttack = 0.0f;

				// }

				// //Attack
				// if (Input.GetKeyDown(KeyCode.J) && m_timeSinceAttack > 0.25f)
				// {
				//     m_currentAttack++;
				//     // Loop back to one after third attack
				//     if (m_currentAttack > 3)
				//         m_currentAttack = 1;

				//     // Reset Attack combo if time since last attack is too large
				//     if (m_timeSinceAttack > 0.5f)
				//         m_currentAttack = 1;

				//     isAttacking = true;

				//     // Perform attack and wait for the attack animation to finish
				//     StartCoroutine(PerformAttack());
				// }

				// DEFINE MORE STATE OF ATTACKING
				// CURRENT STATES: 
				/*
						 walk left (-1)
						 walk right (1)
						 jump (2)
						 fall (-2)
						 idle (0)
						 got hit (-3)
						 attack1 (3) J
						 attack2 (4) K
						 attack3 (5) L
						 ultimate (9) I
				*/

				if (Input.GetKeyDown(KeyCode.J))
				{
					// m_animator.SetTrigger("ultimate");
					isAttacking = true;
					attackSound.Play();
					globalGameState.player2State = 3;
					StartCoroutine(PerformAttack((PLAYER_ATTACK + "1"), nor1Time));

				}
				else if (Input.GetKeyDown(KeyCode.K))
				{
					// m_animator.SetTrigger("ultimate");
					isAttacking = true;
					attackSound.Play();
					globalGameState.player2State = 4;
					StartCoroutine(PerformAttack((PLAYER_ATTACK + "2"), nor2Time));

				}
				else if (Input.GetKeyDown(KeyCode.L))
				{
					// m_animator.SetTrigger("ultimate");
					isAttacking = true;
					attackkSound_1.Play();
					globalGameState.player2State = 5;
					StartCoroutine(PerformAttack((PLAYER_ATTACK + "3"), nor3Time));

				}
				else if (Input.GetKeyDown(KeyCode.I) && (Time.time - lastUltimateTime >= cooldownTime))
				{
					// m_animator.SetTrigger("ultimate");
					isAttacking = true;
					ultimateSound.Play();
					globalGameState.player2State = 9;
					StartCoroutine(PerformUltimate(ultimateTime));
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
						globalGameState.player2State = 2;
						////Debug.Log("Gaara Jump 1");

						m_grounded = false;
						// m_animator.SetBool("Grounded", m_grounded);
						m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
						m_groundSensor.Disable(0.2f);
					}
					else if (canDoubleJump)
					{
						// m_animator.SetTrigger("Jump");
						ChangeAnimationState(PLAYER_JUMP);
						globalGameState.player2State = 2;
						////Debug.Log("Gaara Jump 2");
						m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
						m_jumpsLeft--;
					}
				}
				//Run
				else if (Mathf.Abs(inputX) > Mathf.Epsilon && m_grounded && !isAttacking)
				{
					// Reset timer
					//Debug.Log("Gaara Walk");
					m_delayToIdle = 0.05f;
					// m_animator.SetInteger("AnimState", 1);
					if (inputX == 1f)
					{
						globalGameState.player2State = 1;
					}
					else if (inputX == -1f)
					{
						globalGameState.player2State = -1;
					}
					ChangeAnimationState(PLAYER_RUN);
				}

				//Idle
				else if (m_grounded && !isAttacking && !knockback)
				{
					//Debug.Log("Gaara Idle");
					// Prevents flickering transitions to idle
					m_delayToIdle -= Time.deltaTime;
					// if (m_delayToIdle < 0)
					// m_animator.SetInteger("AnimState", 0);
					if (m_delayToIdle < 0)
					{
						ChangeAnimationState(PLAYER_IDLE);
						globalGameState.player2State = 0;
					}
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
				P1ControlScript hurtComponent = enemy.GetComponent<P1ControlScript>();
				if (hurtComponent != null)
				{
					////Debug.Log("Gaara Attack");
					hurtComponent.Damage(m_facingDirection);
				}
				else
				{
					//Debug.LogError("Player2 component is null");
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

		public void Damage(int m_facingDirection)
		{
			Instantiate(hitParticle, m_animator.transform.position, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));

			if (m_facingDirection == 1 && m_facingDirection == 1 || m_facingDirection != 1 && m_facingDirection != 1)
			{
				playerOnLeft = true;
			}
			if (m_facingDirection != 1 && m_facingDirection == 1 || m_facingDirection == 1 && m_facingDirection != 1)
			{
				playerOnLeft = false;
			}


			// m_animator.SetBool("playerOnLeft", playerOnLeft);
			// m_animator.SetTrigger("damage");
			if (applyKnockback)
			{

				//Knockback
				Knockback(m_facingDirection);
				knockbackSpeedX++;
			}

		}

		public void Knockback(int m_facingDirection)
		{
			knockback = true;
			knockbackStart = Time.time;

			float horizontalForce = knockbackSpeedX * m_facingDirection;

			StartCoroutine(KnockbackCurve(horizontalForce));
			if (playerOnLeft)
			{
				globalGameState.player2State = HURT_LEFT;
				ChangeAnimationState(PLAYER_HURT_LEFT);
			}
			if (!playerOnLeft)
			{
				globalGameState.player2State = HURT_RIGHT;
				ChangeAnimationState(PLAYER_HURT_RIGHT);
			}
			//Debug.Log("Gaara Hurt");
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
			//delay for dead animation show
			float delay = 0.6f;
			Invoke("MoveObjectToSpawnPosition", delay);
		}

		private void MoveObjectToSpawnPosition()
		{
			if (GamePlay.p2Lives > 0)
			{
				knockbackSpeedX = 2;
				knockbackSpeedY = 2;
				StartCoroutine(KeepObjectAtSpawnPosition());
				GamePlay.p2Lives--;
			}
			else
			{
				//Debug.Log("Gaara actually dead");
				//gameObject.SetActive(false);
				Destroy(gameObject);
			}
		}

		private IEnumerator KeepObjectAtSpawnPosition()
		{
			//Debug.Log("Gaara Respawn");
			// Đặt vị trí nhân vật về spawnPosition
			gameObject.transform.position = spawnPosition;
			// Vô hiệu hóa trọng lực
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

			isRespawn = true;
			// Đợi 2 giây
			yield return new WaitForSeconds(1f);
			isRespawn = false;
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;

		}
		public void Die()
		{

			// Play the die animation
			//Debug.Log(gameObject.transform.position.y);
			dieSound.Play();
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
		void UpdateText()
		{
			if(globalGameState.player2IsBeingControlled)
			{
				if (textDame != null)
				{
					textDame.text = globalGameState.p2dame;
				}
				if (textRespawn != null)
				{
					textRespawn.text = globalGameState.p2respawn;
				}
			} else
			{
				if (textDame != null)
				{
					textDame.text = knockbackSpeedX.ToString() + "%";
					globalGameState.p2dame = textDame.text;
				}
				if (textRespawn != null)
				{
					textRespawn.text = "x" + GamePlay.p2Lives.ToString();
					globalGameState.p2respawn = textRespawn.text;
				}
			}
		}

	}
}
