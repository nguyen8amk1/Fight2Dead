using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt2 : MonoBehaviour
{
    [SerializeField]
    private float knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField]
    private bool applyKnockback;
    [SerializeField]
    private GameObject hitParticle;

    private float knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private Hero pc;

    private Animator aliveAnim;
    private Rigidbody2D rbAlive;
    private GameObject aliveGO;
        
    private void Start()
    {

        pc = GameObject.Find("Gaara").GetComponent<Hero>();


        aliveAnim = GetComponent<Animator>();
        rbAlive = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage()
    {
        playerFacingDirection = pc.GetFacingDirection();

        Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockback)
        {
            //Knockback
            Knockback();
        }

    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
}
