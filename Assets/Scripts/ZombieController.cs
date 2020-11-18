using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // Target GO for the zombie to move towards and attack
    private GameObject _Target { get { return GameObject.FindGameObjectWithTag("Player"); } }

    #region Animator members

    // Animation controller
    private Animator _Animator;

    private bool m_IsWalking;
    private bool m_IsRunning;
    private bool m_IsAttacking;
    private bool m_IsAlive;

    // Walking animation
    private bool _IsWalking 
    {
        get { return m_IsWalking; }
        set
        {
            if (m_IsWalking == value)
                return;

            m_IsWalking = value;
            _Animator.SetBool("Walk", m_IsWalking);
        }
    }

    // Running animation
    private bool _IsRunning
    {
        get { return m_IsRunning; }
        set
        {
            if (m_IsRunning == value)
                return;

            m_IsRunning = value;
            _Animator.SetBool("Run", m_IsRunning);
        }
    }

    // Attack animation
    private bool _IsAttacking
    {
        get { return m_IsAttacking; }
        set
        {
            if (m_IsAttacking == value)
                return;

            m_IsAttacking = value;
            _Animator.SetBool("Attack", m_IsAttacking);
        }
    }

    // Death animation when set to false;
    private bool _IsAlive
    {
        get { return m_IsAlive; }
        set
        {
            if (m_IsAlive == value)
                return;

            m_IsAlive = value;
            _Animator.SetBool("Alive", m_IsAlive);
        }
    }

    private const string ANIMATION_WALK_NAME = "Z_Walk_InPlace";
    private const string ANIMATION_RUN_NAME = "Z_Run_InPlace";
    private const string ANIMATION_ATTACK_NAME = "Z_Attack";
    private const string ANIMATION_DEATH_NAME = "Z_FallingBack";

    #endregion

    #region Movement members

    [Header("MUST SET")]
    public float _RotationSpeed;
    public float _DistanceToTarget;
    [Header("SET DYNAMICALLY")]
    public float _MovementSpeed;

    private Quaternion _LookRotation;
    private Vector3 _DirectionRotation;

    private const float IDLE_SPEED = 0.0f;
    private const float WALKING_SPEED = 0.2f;
    private const float RUNNING_SPEED = 0.7f;
    private const float ATTACK_SPEED = 0.3f;

    #endregion

    #region Health members

    private int _Health;

    private const int HEALTH_INIT_VALUE = 3;

    #endregion

    #region Unity Messages

    void Awake()
    {
        // Animation init
        _Animator = GetComponent<Animator>();
        _IsAlive = true;

        // Movement init
        _MovementSpeed = IDLE_SPEED;

        // Health init
        _Health = HEALTH_INIT_VALUE;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
        UpdatePosition();
        UpdateRotation();
    }

    #endregion

    #region Update Methods

    private void UpdatePosition()
    {
        if (!_IsAlive)
            return;

        transform.position 
            = Vector3.Lerp(transform.position, new Vector3(_Target.transform.position.x, transform.position.y, _Target.transform.position.z), Time.deltaTime * _MovementSpeed);
    }

    private void UpdateRotation()
    {
        if (!_IsAlive)
            return;

        _DirectionRotation = (new Vector3(_Target.transform.position.x, transform.position.y, _Target.transform.position.z) - transform.position).normalized;
        _LookRotation = Quaternion.LookRotation(_DirectionRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, _LookRotation, Time.deltaTime * _RotationSpeed);
    }

    private void UpdateAnimations()
    {
        if (_IsAlive)
        {
            _DistanceToTarget = Vector3.Distance(transform.position, _Target.transform.position);

            if (_DistanceToTarget > 15)
            {
                _IsWalking = true;
                _IsRunning = false;
                _IsAttacking = false;
                _MovementSpeed = WALKING_SPEED;

                _Animator.Play(ANIMATION_WALK_NAME);
            }
            else if (_DistanceToTarget <= 15 && _DistanceToTarget > 3)
            {
                _IsWalking = false;
                _IsRunning = true;
                _IsAttacking = false;
                _MovementSpeed = RUNNING_SPEED;

                _Animator.Play(ANIMATION_RUN_NAME);
            }
            else if (_DistanceToTarget <= 2)
            {
                _IsWalking = false;
                _IsRunning = false;
                _IsAttacking = true;
                _MovementSpeed = _DistanceToTarget <= 1.5 ? IDLE_SPEED : ATTACK_SPEED;

                _Animator.Play(ANIMATION_ATTACK_NAME);
            }
        }
        
        if(_IsAlive && _Health <= 0)
        {
            _IsAlive = false;
            _IsWalking = false;
            _IsRunning = false;
            _IsAttacking = false;
            _MovementSpeed = IDLE_SPEED;

            _Animator.Play(ANIMATION_DEATH_NAME);
            Invoke("OnCompletedDeathAnimation", 1.2f);
        }
    }

    // Runs after the death animation is completed.
    private void OnCompletedDeathAnimation()
    {
        Destroy(gameObject);
    }

    #endregion

    public void BulletCollision()
    {
        _Health--;
    }
}
