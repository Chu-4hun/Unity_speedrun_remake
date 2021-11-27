using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 1f;
        public bool isGround = false;
        public int coins = 0;

        public int strength = 10;
        public int attackCooldown = 1;
        public float attackableRange = 0.7f;
        public GameObject attackPoint;
        public LayerMask attackableLayer;


        [SerializeField] private float _sprintSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform cameraTransform;

        private bool canAttack = true;
        private Rigidbody rb;
        private Animator animator;
        private float velocity;

        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int IsInAir = Animator.StringToHash("isInAir");
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        void Start()
        {
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            speed = Input.GetButton("Sprint") ? _sprintSpeed : 1f;
            animator.SetFloat(Speed, 0);

            Vector3 movementVec = new Vector3(horizontal, 0f, vertical);

            if (movementVec.magnitude > 0)
            {
                animator.SetFloat(Speed, speed);
                movementVec.Normalize();
                movementVec *= speed * Time.deltaTime;
                movementVec = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementVec;
                transform.Translate(movementVec, Space.World);
            }

            if (movementVec != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementVec, Vector3.up);

                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetButton("Attack") && canAttack)
            {
                Attack();
                animator.SetBool(AttackAnim, !canAttack);
            }
            else
            {
                animator.SetBool(AttackAnim, !canAttack);
            }

            if (Input.GetButton("Jump"))
            {
                if (isGround)
                {
                    rb.AddForce(Vector3.up * (jumpForce * Time.deltaTime), ForceMode.Impulse);
                }
            }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void Attack()
        {
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

        private IEnumerator AttackCountdown()
        {
            int Counter = attackCooldown;
            while (Counter > 0)
            {
                yield return new WaitForSeconds(1);
                Counter--;
            }

            canAttack = true;
        }


        void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Ground")) isGround = true;
            animator.SetBool(IsInAir, !isGround);
        }

        void OnTriggerExit(Collider col)
        {
            if (col.CompareTag("Ground")) isGround = false;
            animator.SetBool(IsInAir, !isGround);
        }


        // Update is called once per frame
    }
}