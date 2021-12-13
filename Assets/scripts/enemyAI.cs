using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class enemyAI : Unit, IAttackable
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private LayerMask _player_mask;
    private float _distanceToPlayer;
    private void Start()
    {
        _agent.speed = speed;
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (HP <= 0)
        {
            Death();
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _agent.SetDestination(col.transform.position);
            _distanceToPlayer = Vector3.Distance(this.transform.position, col.transform.position);


            // Physics.Raycast(this.transform.position,col.transform.TransformDirection(Vector3.forward),15, _player_mask)
            if (_distanceToPlayer < 2)
            {
                _agent.isStopped = true;
                Debug.Log("Enemy stopped by " + col.name);
            }

            if (_agent.isStopped && canAttack)
            {
                Debug.Log("Enemy trying to attack " + col.name);
                attack();
                _agent.isStopped = false;
            }
        }
    }


    protected override void StartAnimAttack()
    {
    }

    protected override void EndAnimAttack()
    {
    }

    protected override void SetAnimIsInAir(bool _isGround)
    {
    }

    protected override void SetAnimSpeed(float _speed)
    {
    }

    public void DealDamage(int count)
    {
        HP -= count;
    }
}