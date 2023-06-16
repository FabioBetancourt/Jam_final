using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerInput playerInput;
        private Vector2 _movementInput;
        private Rigidbody rb;
        public float speed = 5.0f;
        public bool isOnGround = true;
        public float accelaration = 0.1f;
        public float decelaration = 0.1f;

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
            }else
            {
                rb.velocity = Vector3.Lerp( rb.velocity, new Vector3( movement.x, rb.velocity.y, movement.z), decelaration);
            }

            //new Vector3(movement.x, rb.velocity.y, movement.z);
            
            if (playerInput.actions["Jump"].triggered && isOnGround)
            {
                rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                isOnGround = false;
            }
        }

        /*
        private bool IsGrounded()
        {
            float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
            Ray ray = new Ray(transform.position, Vector3.down);
            return Physics.Raycast(ray, distance);
        }
        */
        
        private void OnCollisionEnter(Collision other)
        {
            isOnGround = true;
        }
    }
}