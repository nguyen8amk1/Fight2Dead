using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
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
    public float attackoffset;
    public LayerMask enemyLayers;
    private bool isAttacking = false;
    private Hurt3 hurt3;
    // Use this for initializationz
    public int GetFacingDirection()
    {
        return m_facingDirection;
    }
    void Start()
    {
        //test
        hurt3 = GetComponent<Hurt3>();

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor>();
    }

    // Update is called once per frame
    void Update()
    {
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

        // Move
        if (!isAttacking && !hurt3.checkKnock())
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --

        // //Hurt
        // if (Input.GetKeyDown("q"))
        //     m_animator.SetTrigger("Hurt");

        IEnumerator PerformAttack()
        {
            m_body2d.velocity = Vector2.zero;
            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            Attack();

            // Wai until the animation attack end
            yield return new WaitForSeconds(0.6f);

            // Reset isAttacking = false
            isAttacking = false;

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }
        //Attack
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
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
        //Jump
        // else if (Input.GetKeyDown("space") && m_grounded)
        // {
        //     m_animator.SetTrigger("Jump");
        //     m_grounded = false;
        //     m_animator.SetBool("Grounded", m_grounded);
        //     m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        //     m_groundSensor.Disable(0.2f);
        // }
        // else if (Input.GetKeyDown("space"))
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
    // protected void Attack()
    // {
    //     Collider2D[] collidersToPush = new Collider2D[10];
    //     ContactFilter2D filter = new ContactFilter2D();
    //     filter.useTriggers = true;
    //     int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToPush);
    //     for (int i = 0; i < colliderCount; i++)
    //     {
    //         Rigidbody2D rigidBody = collidersToPush[i].GetComponent<Rigidbody2D>();
    //         if (rigidBody)
    //         {
    //             Vector2 direction = (collidersToPush[i].transform.position - transform.position).normalized;
    //             rigidBody.AddForce(direction * pushForce, ForceMode2D.Impulse);
    //             Debug.Log("Hit");
    //         }
    //     }
    // }

    // private void Attack()
    // {
    //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    //     foreach (Collider2D enemy in hitEnemies)
    //     {
    //         // GetComponent<Collider>().transform.parent.SendMessage("Damage");
    //         enemy.transform.parent.SendMessage("Damage");
    //     }
    // }
    // void OnDrawGizmosSelected()
    // {
    //     if (attackPoint == null)
    //         return;
    //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }
    private void Attack()
    {
        Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f); // Hướng của nhân vật
        Vector2 attackPointPosition = (Vector2)transform.position + attackDirection * attackoffset; // Tính toán vị trí của attackPoint
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointPosition, attackRange, enemyLayers); // Sử dụng vị trí tính toán được để tấn công
        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log("Enemy object: " + enemy);
            // Debug.Log("Enemy object: " + transform.parent);
            // enemy.GetComponent<Hurt1>().Damage();
            Hurt1 hurtComponent = enemy.GetComponent<Hurt1>();
            if (hurtComponent != null)
            {

                hurtComponent.Damage();
            }
            else
            {
                Debug.LogError("Hurt1 component is null");
            }
            //enemy.transform.parent.SendMessage("Damage");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Vector2 attackDirection = new Vector2(GetFacingDirection(), 0f);
        Vector2 attackPointPosition = (Vector2)transform.position + attackDirection * attackoffset;
        Gizmos.DrawWireSphere(attackPointPosition, attackRange);
    }


}
