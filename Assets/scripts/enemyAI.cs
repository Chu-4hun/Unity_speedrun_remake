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
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float Distance;

    private void Start()
    {
        _agent.speed = speed;
        // startingPosition = transform.position;
        // roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
        
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f,70f);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    void OnTriggerStay(Collider col)
    {
        Debug.Log("Enemy saw" + col.name);
        if (col.CompareTag("Player") )
        {
            _agent.SetDestination(col.transform.position);
            // if (Physics.Raycast(this.transform.position,col.transform.TransformDirection(Vector3.forward),10, "Player"))
            // {
            //     
            // }
            if (_agent.isStopped)
            {
                attack();
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