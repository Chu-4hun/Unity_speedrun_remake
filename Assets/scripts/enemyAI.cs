using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class enemyAI : Unit
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private LayerMask _player_mask;
    private float _distanceToPlayer;

    // private Vector3 startingPosition;
    // private Vector3 roamingPosition;
    // private float Distance;


    private void Start()
    {
        _agent.speed = speed;
        // startingPosition = transform.position;
        // roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
    }

    // private Vector3 GetRoamingPosition()
    // {
    //     return startingPosition + GetRandomDir() * Random.Range(10f,70f);
    // }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void FixedUpdate()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            _agent.SetDestination(col.transform.position);
            _distanceToPlayer = Vector3.Distance(col.transform.position, transform.position);
            

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
}