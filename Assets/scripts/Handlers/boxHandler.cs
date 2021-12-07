using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxHandler : MonoBehaviour, IAttackable

{
    public int Health = 30;

    public ParticleSystem DamageParticle;

    public GameObject[] CoinSpawnPoints;

    public GameObject Coin;

    private void FixedUpdate()
    {
        if (Health < -0) DestroyBox();
    }

    private void DestroyBox()
    {
        DamageParticle.transform.parent = null;
        DamageParticle.Play();

        foreach(GameObject CoinSpawnPoint in CoinSpawnPoints)
        {
            CoinSpawnPoint.transform.parent = null;
            Instantiate(Coin, CoinSpawnPoint.transform);
        }

        Destroy(gameObject);
    }

    public void DealDamage(int Count)
    {
        Health -= Count;
        DamageParticle.Play();
    }
}