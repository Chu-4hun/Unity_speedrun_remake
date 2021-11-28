using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class enemyAI : Unit
{
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float Distance;
    

    private void Start()
    {
        startingPosition = transform.position;
        roamingPosition = GetRoamingPosition();
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
        if (col.CompareTag("Player") )
        {
            
        }
    }

    protected override void StartAnimAttack()
    {
        throw new NotImplementedException();
    }

    protected override void EndAnimAttack()
    {
        throw new NotImplementedException();
    }
}