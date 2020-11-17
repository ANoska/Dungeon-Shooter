using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float _DistanceToTarget;

    // Animation controller
    private Animator _Animator;

    // Target GO for the zombie to move towards and attack
    private GameObject _Target { get { return GameObject.FindGameObjectWithTag("Player"); } }

    // Walking animation
    private bool _IsWalking;

    // Running animation
    private bool _IsRunning;

    // Attack animation
    private bool _IsAttacking;

    // Death animation when set to false;
    private bool _IsAlive;

    void Awake()
    {
        _IsAlive = true;
        _Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsAlive)
        {
            _DistanceToTarget = Vector3.Distance(transform.position, _Target.transform.position);

            if (_DistanceToTarget > 15)
            {
                _IsWalking = true;
                _IsRunning = false;
                _IsAttacking = false;
            }
            else if (_DistanceToTarget <= 15 && _DistanceToTarget > 3)
            {
                _IsWalking = false;
                _IsRunning = true;
                _IsAttacking = false;
            }
            else if (_DistanceToTarget <= 3)
            {
                _IsWalking = false;
                _IsRunning = false;
                _IsAttacking = true;
            }
        }
        else
        {
            _IsWalking = false;
            _IsRunning = false;
            _IsAttacking = false;


            _Animator.SetBool("Alive", false);
            _Animator.Play("Z_FallingBack");
        }

        _Animator.SetBool("Alive", _IsAlive);
        _Animator.SetBool("Walk", _IsWalking);
        _Animator.SetBool("Run", _IsRunning);
        _Animator.SetBool("Attack", _IsAttacking);
    }
}
