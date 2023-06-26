using System;
using Bullets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour, IShooter
    {
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float rotationSpeed = 5f;
        //[SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform barrelTransform;
        //[SerializeField] private Transform bulletParent;
        [SerializeField] private float bulletHitMissDistance = 25f;
        [SerializeField] private AudioClip shootClip;
        private AudioSource audioSource;

        //variables for the player movement
        private CharacterController controller;
        private PlayerInput playerInput;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        //position of the camera
        private Transform cameraTransform;
        //input actions for the player
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction shootAction;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            //reading the position of the camera
            cameraTransform = Camera.main.transform;
            //getting the input actions
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            shootAction = playerInput.actions["Shoot"];
            //locked the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
            audioSource = GetComponent<AudioSource>();
        }
        
        private void OnEnable()
        {
            shootAction.performed += _ => Shoot();
        }

        private void OnDisable()
        {
            shootAction.performed -= _ => Shoot();
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            //movement with the camera direction
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * (Time.deltaTime * playerSpeed));

            // Changes the height position of the player..
            if (jumpAction.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            //rotate towards camera direction
            Quaternion targetRotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public void Shoot()
        {
            RaycastHit hit;
            //GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
            GameObject bullet = BulletPool.Instance.Get();
            bullet.transform.position = barrelTransform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true); 
            audioSource.PlayOneShot(shootClip);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;
            }
            else
            {
                bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
                bulletController.hit = false;
            }
        }
    }
}