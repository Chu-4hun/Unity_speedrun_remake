using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected int strength = 10;
    protected int attackCooldown = 1;
    [SerializeField] protected GameObject attackPoint;
    [SerializeField] protected LayerMask attackableLayer;
    protected float attackableRange = 0.3f;
    
    private bool canAttack = true;
    
    protected abstract void StartAnimAttack();
    protected abstract void EndAnimAttack();

    protected void attack()
    {
        StartAnimAttack();
        Collider[] hittedColliders =
            Physics.OverlapSphere(attackPoint.transform.position, attackableRange, attackableLayer);
        foreach (Collider hittedCollider in hittedColliders)
        {
            IAttackable attackable = hittedCollider.gameObject.GetComponent<IAttackable>();
            attackable.DealDamage(strength);
            Debug.Log(hittedCollider.name + "deal" + strength + "damage");
        }

        canAttack = false;
        StartCoroutine(AttackCountdown());
    }

    public bool isCanAttack()
    {
        return canAttack;
    }

    IEnumerator AttackCountdown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        EndAnimAttack();
    }
}