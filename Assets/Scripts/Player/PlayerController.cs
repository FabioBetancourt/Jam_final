using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Dont touch this")]
        [Tooltip("Really dont touch this")]
        public PlayerInput playerInput;
        [Header("Speed")]
        [Tooltip("This is the speed of the player")]
        public float speed = 5.0f;
        [Header("Acceleration")]
        [Tooltip("This is the acceleration of the player, the higher the number the faster the player will reach max speed")]
        public float accelaration = 0.1f;
        [FormerlySerializedAs("decelaration")]
        [Header("Deceleration")]
        [Tooltip("This is the deceleration of the player, the higher the number the faster the player will stop")]
        public float deceleration = 0.1f;
        [Tooltip("This is the force of the jump, the higher the number the higher the jump")]
        public float forceJump = 5f;
        private Vector2 _movementInput;
        private Rigidbody rb;
        private bool isGround;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y) * (speed);
            if (movement.magnitude > 0.1f)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, new Vector3( movement.x, rb.velocity.y, movement.z), accelaration);
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
            }else
            {
                rb.velocity = Vector3.Lerp( rb.velocity, new Vector3( movement.x, rb.velocity.y, movement.z), deceleration);
            }
            
            if (playerInput.actions["Jump"].triggered && isGround)
            {
                rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                isGround = false;
                
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            isGround = true;
        }
    }
    
    
}