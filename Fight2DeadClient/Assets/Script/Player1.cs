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
    public float attackRange;
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

    // Use this for initializationz
    public int GetFacingDirection()
    {
        return m_facingDirection;
    }
    void Start()
    {

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor>();
        m_animator.SetTrigger("intro");
    }

    // Update is called once per frame
    void Update()
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
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }



        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --

        // //Hurt
        // if (Input.GetKeyDown("q"))
        //     m_animator.SetTrigger("Hurt");

        // Move
        if (!isAttacking && !knockback)
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }
        IEnumerator PerformAttack()
        {
            m_body2d.velocity = Vector2.zero;
            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            // Debug.Log(m_animator.GetFloat("Attack.Active"));

            // Attack();

            // Wai until the animation attack end
            yield return new WaitForSeconds(0f);

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

            /*
            if (animator.GetFloat("Weapon.Active") > 0f)
            {
                Attack();
            }
            */

            // // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            // m_animator.SetTrigger("Attack" + m_currentAttack);
            // Attack();

            // // Reset timer
            // m_timeSinceAttack = 0.0f;

            isAttacking = true;
            // Thực hiện tấn công và chờ kết thúc animation tấn công
            StartCoroutine(PerformAttack());
        }

        // Block
        else if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        else if (Input.GetKeyDown("w"))
        {
            if (m_grounded)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }
            else if (canDoubleJump)
            {
                m_animator.SetTrigger("Jump");
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_jumpsLeft--;
            }
        }
        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    // private void Attack()
    // {
    //     Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f); // Hướng của nhân vật
    //     Vector2 attackPointPosition = (Vector2)transform.position + attackDirection * attackoffset; // Tính toán vị trí của attackPoint
    //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointPosition, attackRange, enemyLayers); // Sử dụng vị trí tính toán được để tấn công
    //     foreach (Collider2D enemy in hitEnemies)
    //     {

    //         Player2 hurtComponent = enemy.GetComponent<Player2>();
    //         if (hurtComponent != null)
    //         {

    //             Debug.Log(attackRange);
    //             Debug.Log(attackoffset);
    //             Debug.Log("Gaara Attack");
    //             hurtComponent.Damage(m_facingDirection);

    //         }
    //         else
    //         {
    //             Debug.LogError("Player2 component is null");
    //         }

    //     }
    // }

    // void OnDrawGizmosSelected()
    // {
    //     if (attackPoint == null)
    //         return;
    //     Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f);
    //     Vector2 attackPointPosition = (Vector2)transform.position + attackDirection * attackoffset;
    //     Gizmos.DrawWireSphere(attackPointPosition, attackRange);
    // }
    private void Attack()
    {
        Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f); // Hướng của nhân vật
        Vector2 attackPointPosition = (Vector2)transform.position + new Vector2(attackOffsetX * attackDirection.x, attackOffsetY); // Tính toán vị trí của attackPoint
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointPosition, attackRange, enemyLayers); // Sử dụng vị trí tính toán được để tấn công
        foreach (Collider2D enemy in hitEnemies)
        {
            Player2 hurtComponent = enemy.GetComponent<Player2>();
            if (hurtComponent != null)
            {
                Debug.Log(attackRange);
                Debug.Log(attackOffsetX);
                Debug.Log(attackOffsetY);
                Debug.Log("Gaara Attack");
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
        Gizmos.DrawWireSphere(attackPointPosition, attackRange);
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


        m_animator.SetBool("playerOnLeft", playerOnLeft);
        m_animator.SetTrigger("damage");

        if (applyKnockback)
        {
            //Knockback
            Knockback(playerFacingDirection);
            knockbackSpeedX++;
        }

    }

    // public void Knockback(int playerFacingDirection)
    // {
    //     knockback = true;
    //     knockbackStart = Time.time;
    //     Debug.Log("Knockback: " + m_body2d.velocity);
    //     m_body2d.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);

    //     Debug.Log("Knockback: " + m_body2d.velocity);
    // }
    public void Knockback(int playerFacingDirection)
    {
        knockback = true;
        knockbackStart = Time.time;

        float horizontalForce = knockbackSpeedX * playerFacingDirection;

        StartCoroutine(KnockbackCurve(horizontalForce));
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
    }

    public void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            m_body2d.velocity = new Vector2(0.0f, m_body2d.velocity.y);
        }
    }
    public void UpdateAttackOffsetX(float x)
    {
        attackOffsetX = x;
    }
    public void UpdateAttackOffsetY(float y)
    {
        attackOffsetY = y;
    }
    public void UpdateAttackRange(float range)
    {
        attackRange = range;
    }
}
