using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    
    protected int strength = 10; //Damage per 1 hit
    protected int attackCooldown = 1;
    
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float jumpForce = 5f;
    [SerializeField] protected GameObject attackPoint;
    [SerializeField] protected LayerMask attackableLayer;
    protected float attackableRange = 0.3f;

    private bool canAttack = true;
    private bool isGround;

    protected abstract void StartAnimAttack();
    protected abstract void EndAnimAttack();
    protected abstract void SetAnimIsInAir(bool _isGround);
    protected abstract void SetAnimSpeed(float _speed);


    protected virtual void moveTo(float horizontal, float vertical, float speed)
    {
        SetAnimSpeed(0f);
        Vector3 movementVec = new Vector3(horizontal, 0f, vertical);

        if (movementVec.magnitude > 0)
        {
            SetAnimSpeed(speed);
            movementVec.Normalize();
            movementVec *= speed * Time.deltaTime;
            transform.Translate(movementVec, Space.World);
        }
    }
    public bool isCanAttack()
    {
        return canAttack;
    }
    
    protected void jump()
    {
        if (isGround)
        {
            rb.AddForce(Vector3.up * (jumpForce * Time.deltaTime), ForceMode.Impulse);
        }
    }
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
    //Кулдаун 
    //Cooldown
    IEnumerator AttackCountdown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        EndAnimAttack();
    }
    
    //Проверка на на земле ли unit
    //Сheck is character on the ground 
     void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Ground")) isGround = true;
        SetAnimIsInAir(isGround);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Ground")) isGround = false;
        SetAnimIsInAir(isGround);
    }
}