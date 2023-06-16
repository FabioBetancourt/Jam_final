using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerInput playerInput;
        private Vector2 _movementInput;
        private Rigidbody rb;
        public float speed = 5.0f; // Velocidad de movimiento

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
            Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y) * (speed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + movement);

            if (playerInput.actions["Jump"].triggered && IsGrounded())
            {
                rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }
        }

        private bool IsGrounded()
        {
            float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
            Ray ray = new Ray(transform.position, Vector3.down);
            return Physics.Raycast(ray, distance);
        }
    }
}