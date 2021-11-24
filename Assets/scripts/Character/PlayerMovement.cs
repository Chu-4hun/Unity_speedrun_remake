using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 1f;
        public bool isGround = false;
        public int coins = 0;
        [SerializeField] private float _sprintSpeed = 5f;
        [SerializeField] private float jumpForce  = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform cameraTransform;
        private Rigidbody rb;
        private Animator animator;
        private float velocity;
        
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int IsInAir = Animator.StringToHash("isInAir");
        private static readonly int Jump = Animator.StringToHash("jump");

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


            // float velocityZ = Vector3.Dot(movementVec.normalized, transform.forward);
            // float velocityX = Vector3.Dot(movementVec.normalized, transform.right);
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


        void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag($"Ground")) isGround = true;
            animator.SetBool(IsInAir, !isGround);
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag($"Ground")) isGround = false;
            animator.SetBool(IsInAir, !isGround);
        }
        
        
    

        // Update is called once per frame
    }
}