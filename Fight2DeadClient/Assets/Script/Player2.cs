using SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player2 : MonoBehaviour
{
    //Movement
    [SerializeField] public float m_speed = 4.0f;
    [SerializeField] public float m_jumpForce = 7.5f;
    public Animator m_animator;
    public Rigidbody2D m_body2d;
    public Sensor m_groundSensor;
    public bool m_grounded = false;
    public int m_facingDirection = 1;
    public int m_currentAttack = 0;
    public float m_timeSinceAttack = 0.0f;
    public float m_delayToIdle = 0.0f;

    //DOUBLE JUMP
    public int m_jumpsLeft = 2;
    public bool canDoubleJump;
    //FIGHT1
    public Transform attackPoint;
    public float attackRangeX;
    public float attackRangeY;
    [SerializeField] public float attackOffsetX;
    [SerializeField] public float attackOffsetY;
    public LayerMask enemyLayers;
    public bool isAttacking = false;

    //Hurt
    [SerializeField]
    public float knockbackSpeedX, knockbackSpeedY, knockbackDuration;
    [SerializeField]
    public bool applyKnockback;
    [SerializeField]
    public GameObject hitParticle;
    public float knockbackStart;
    public bool playerOnLeft;
    public bool knockback = false;

    //Ultimate cooldown
    public float cooldownTime = 10f;
    public float lastUltimateTime = 0f;

    //Spawn
    public GameObject gameObject;
    public Vector3 spawnPosition;
    public int numberRespawn = 1;
    public bool isRespawn = false;
    //animation
    public string currentState;
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
    const string PLAYER_ULTIMATE = "Ultimate";
    const string PLAYER_SWITCH = "SwitchPlayer";
    public float animTime;
    //time animation of attack
    public float nor1Time;
    public float nor2Time;
    public float nor3Time;
    public float ultimateTime;
    //sound effect
    public AudioSource attackSound;
    public AudioSource attackkSound_1;
    public AudioSource ultimateSound;
    public AudioSource dieSound;

    //tanker check
    public bool isTank;

    //dame UI
    public Text textDame;
    public Text textRespawn;

    public int playerFacingDirection;
    public int mylayerFacingDirection;

    void Start()
    {

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor>();
        // m_animator.SetTrigger("intro");
        // ChangeAnimationState(PLAYER_INTRO);

    }    
}

