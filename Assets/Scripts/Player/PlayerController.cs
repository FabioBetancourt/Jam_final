using System;
using Bullets;
using Enemies;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour, IShooter, IAttacker, IDamageable
    {
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;

        [SerializeField] private float rotationSpeed = 5f;

        [SerializeField] private Transform barrelTransform;

        [SerializeField] private float bulletHitMissDistance = 25f;
        [SerializeField] private AudioClip shootClip;
        [SerializeField] private float initialHealth = 100f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private Animator anim;

        private AudioSource _audioSource;

        private CharacterController _controller;
        private PlayerInput _playerInput;
        private Vector3 _playerVelocity;

        private bool _groundedPlayer;

        private Transform _cameraTransform;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _shootAction;
        private bool _isInDragonTrigger = false;
        private Enemy _dragon;

        public float Health { get; private set; }
        public float Damage { get; private set; }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();

            _cameraTransform = Camera.main.transform;

            _moveAction = _playerInput.actions["Move"];
            _jumpAction = _playerInput.actions["Jump"];
            _shootAction = _playerInput.actions["Shoot"];

            Cursor.lockState = CursorLockMode.Locked;
            _audioSource = GetComponent<AudioSource>();

            anim = GetComponent<Animator>();

            Health = initialHealth;
            Damage = damage;
        }

        private void OnEnable()
        {
            _shootAction.performed += _ => Shoot();
        }

        private void OnDisable()
        {
            _shootAction.performed -= _ => Shoot();
        }

        void Update()
        {
            _groundedPlayer = _controller.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }

            Vector2 input = _moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * _cameraTransform.right.normalized + move.z * _cameraTransform.forward.normalized;
            move.y = 0f;
            _controller.Move(move * (Time.deltaTime * playerSpeed));
            anim.SetFloat("VelocityX", input.x);
            anim.SetFloat("VelocityY", input.y);
            anim.SetFloat("idle", move.magnitude);
            anim.SetBool("IsonFloor", !_groundedPlayer);

            if (_jumpAction.triggered && _groundedPlayer)
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                anim.SetBool("IsonFloor", true);
            }

            _playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);


            Quaternion targetRotation = Quaternion.Euler(0f, _cameraTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public void Shoot()
        {
            RaycastHit hit;

            GameObject bullet = BulletPool.Instance.Get();
            bullet.transform.position = barrelTransform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            _audioSource.PlayOneShot(shootClip);

            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.damage = Damage; // Added line

            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;

                IDamageable target = hit.transform.GetComponent<IDamageable>();
                if (target != null)
                {
                    Attack(target);
                }
            }
            else
            {
                bulletController.target = _cameraTransform.position + _cameraTransform.forward * bulletHitMissDistance;
                bulletController.hit = false;
            }
        }

        public void Attack(IDamageable target)
        {
            target.TakeDamage(Damage);
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;
            print(Health);
            if (Health <= 0)
            {
                SceneManager.LoadScene("Defeat");
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Dragon"))
            {
                _dragon = other.gameObject.GetComponent<Enemy>();
                _isInDragonTrigger = true;
                _dragon.PlayerInTrigger(_isInDragonTrigger);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Dragon"))
            {
                _isInDragonTrigger = false;
                _dragon.PlayerInTrigger(_isInDragonTrigger);
            }
        }
    }
}