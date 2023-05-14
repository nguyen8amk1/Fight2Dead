using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt3 : MonoBehaviour
{
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

    private Hero1 pc;
    private Hero me;
    private Animator aliveAnim;
    private Rigidbody2D rbAlive;
    private void Start()
    {

        pc = GameObject.Find("Luffy").GetComponent<Hero1>();
        me = GetComponent<Hero>();
        // aliveGO = transform.Find("Alive").gameObject;

        aliveAnim = GetComponent<Animator>();
        rbAlive = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckKnockback();
    }
    public bool checkKnock(){
        return knockback;
    }
    public void Damage()
    {
        playerFacingDirection = pc.GetFacingDirection();
        mylayerFacingDirection = me.GetFacingDirection();
        Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1 && mylayerFacingDirection==1 || playerFacingDirection != 1 && mylayerFacingDirection!=1)
        {
            playerOnLeft = true;
        }
        if (playerFacingDirection != 1 && mylayerFacingDirection==1 ||playerFacingDirection == 1 && mylayerFacingDirection!=1 )
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

    public void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        Debug.Log("Knockback: " + rbAlive.velocity);
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        Debug.Log("Knockback: " + rbAlive.velocity);
    }

    public void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }
}
