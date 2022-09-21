using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class Coin_handler : MonoBehaviour
{
    public ParticleSystem destroyParticle;
    public int ScorePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            playerScript.coins += ScorePoint;
            Debug.Log(ScorePoint + " Added");
            destroyParticle.transform.parent = transform.parent;
            destroyParticle.Play();
            Destroy(gameObject);
        }
    }
}