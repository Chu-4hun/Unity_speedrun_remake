using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float jumpForce  = 5f;
    [SerializeField] private LayerMask _aniLayerMask;
    [SerializeField] private Transform cameraTransform;

    Animator _animatior;
    private Rigidbody rb;
    private bool _isGround = false;

    private void Awake()
    {
        _animatior = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= _speed * Time.deltaTime;
            movement = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movement;
            transform.Translate(movement, Space.World);
        }


        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        if (Input.GetButton("Jump"))
        {
            if (_isGround)
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
        if (other.gameObject.CompareTag($"Ground")) _isGround = true;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag($"Ground")) _isGround = false;
    }
    

    // Update is called once per frame
}