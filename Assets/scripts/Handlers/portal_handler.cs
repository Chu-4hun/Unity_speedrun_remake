using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal_handler : MonoBehaviour
{
    [SerializeField] private int levelIndex = 1;
    [SerializeField] private int  coinsToCompete = 10;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.GetComponent<PlayerMovement>().coins >= coinsToCompete)
            {
                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
