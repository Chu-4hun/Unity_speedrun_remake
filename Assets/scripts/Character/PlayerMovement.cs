using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerMovement : Unit, IAttackable
    {
        public int coins = 0;


        [SerializeField] private float _sprintSpeed = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform cameraTransform;

        private Animator animator;

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

        protected override void Death()
        {
            Debug.Log("Player is dead");
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float speed = Input.GetButton("Sprint") ? _sprintSpeed : 1f;
            moveTo(horizontal, vertical, speed);

            if (HP <= 0)
            {
                Death();
            }

            if (Input.GetButton("Attack") && isCanAttack())
            {
                attack();
            }

            if (Input.GetButton("Jump"))
            {
                jump();
            }
        }

        protected override void moveTo(float horizontal, float vertical, float speed)
        {
            SetAnimSpeed(0f);
            Vector3 movementVec = new Vector3(horizontal, 0f, vertical);

            if (movementVec.magnitude > 0)
            {
                SetAnimSpeed(speed);
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
        }


        // Update is called once per frame
        protected override void StartAnimAttack()
        {
            animator.SetBool(AttackAnim, isCanAttack());
        }

        protected override void EndAnimAttack()
        {
            animator.SetBool(AttackAnim, false);
        }

        protected override void SetAnimSpeed(float _speed)
        {
            animator.SetFloat(Speed, _speed);
        }

        protected override void SetAnimIsInAir(bool isGround)
        {
            animator.SetBool(IsInAir, !isGround);
        }

        public void DealDamage(int count)
        {
            HP -= count;
        }
    }
}