using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSounds : MonoBehaviour
{
    public CharacterController controller;
    public Transform ground;

    public AudioSource pasos;
    public AudioSource salto;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private bool isMoving = false; // Variable para rastrear si el jugador se está moviendo

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];

        _moveAction.performed += ctx => CheckForMovement();
        _moveAction.canceled += ctx => CheckForMovement();

        _jumpAction.performed += ctx => PlayJumpSound();
    }

    private void CheckForMovement()
    {
        float movementTolerance = 0.1f;
        Vector2 move = _moveAction.ReadValue<Vector2>();

        if (move.magnitude > movementTolerance)
        {
            isMoving = true;

            if (!pasos.isPlaying && !salto.isPlaying)
            {
                pasos.Play();
            }
        }
        else
        {
            isMoving = false;
            pasos.Pause();
        }
    }

    private void PlayJumpSound()
    {
        if (!salto.isPlaying)
        {
            salto.Play();
            pasos.Pause();
            StartCoroutine(WaitForJumpSound());
        }
    }

    private IEnumerator WaitForJumpSound()
    {
        while (salto.isPlaying)
        {
            yield return null;
        }

        if (isMoving)
        {
            pasos.UnPause();
        }
    }
}