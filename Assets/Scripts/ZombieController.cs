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

    // Walking animation
    private bool _IsWalking;

    // Running animation
    private bool _IsRunning;

    // Attack animation
    private bool _IsAttacking;

    // Death animation when set to false;
    private bool _IsAlive;

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

    void Awake()
    {
        _IsAlive = true;
        _MovementSpeed = IDLE_SPEED;
        _Animator = GetComponent<Animator>();
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

    private void UpdatePosition()
    {
        transform.position 
            = Vector3.Lerp(transform.position, new Vector3(_Target.transform.position.x, transform.position.y, _Target.transform.position.z), Time.deltaTime * _MovementSpeed);
    }

    private void UpdateRotation()
    {
        _DirectionRotation = (new Vector3(_Target.transform.position.x, transform.position.y, _Target.transform.position.z) - transform.position).normalized;
        _LookRotation = Quaternion.LookRotation(_DirectionRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, _LookRotation, Time.deltaTime * _RotationSpeed);
    }

    void UpdateAnimations()
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

                _Animator.SetBool("Alive", _IsAlive);
                _Animator.SetBool("Walk", _IsWalking);
                _Animator.SetBool("Run", _IsRunning);
                _Animator.SetBool("Attack", _IsAttacking);

                _Animator.Play(ANIMATION_WALK_NAME);
            }
            else if (_DistanceToTarget <= 15 && _DistanceToTarget > 3)
            {
                _IsWalking = false;
                _IsRunning = true;
                _IsAttacking = false;
                _MovementSpeed = RUNNING_SPEED;

                _Animator.SetBool("Alive", _IsAlive);
                _Animator.SetBool("Walk", _IsWalking);
                _Animator.SetBool("Run", _IsRunning);
                _Animator.SetBool("Attack", _IsAttacking);

                _Animator.Play(ANIMATION_RUN_NAME);
            }
            else if (_DistanceToTarget <= 2)
            {
                _IsWalking = false;
                _IsRunning = false;
                _IsAttacking = true;
                _MovementSpeed = _DistanceToTarget <= 1.5 ? IDLE_SPEED : ATTACK_SPEED;

                _Animator.SetBool("Alive", _IsAlive);
                _Animator.SetBool("Walk", _IsWalking);
                _Animator.SetBool("Run", _IsRunning);
                _Animator.SetBool("Attack", _IsAttacking);

                _Animator.Play(ANIMATION_ATTACK_NAME);
            }
        }
        else
        {
            _IsWalking = false;
            _IsRunning = false;
            _IsAttacking = false;
            _MovementSpeed = IDLE_SPEED;


            _Animator.SetBool("Alive", _IsAlive);
            _Animator.SetBool("Walk", _IsWalking);
            _Animator.SetBool("Run", _IsRunning);
            _Animator.SetBool("Attack", _IsAttacking);

            _Animator.Play(ANIMATION_DEATH_NAME);
        }
    }
}
