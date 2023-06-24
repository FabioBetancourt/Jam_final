using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
namespace Player
{
    public class SwitchVCam : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput playerInput;
        [SerializeField]
        private int priority = 10;
        [SerializeField]
        private Canvas aimCanvas;

        private InputAction aimAction;
        private CinemachineVirtualCamera virtualCamera;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            aimAction = playerInput.actions["Aim"];
        }

        private void OnEnable()
        {
            aimAction.performed += _ => StarAim();
            aimAction.canceled += _ => StopAim();
        }
        
        private void OnDisable()
        {
            aimAction.performed -= _ => StarAim();
            aimAction.canceled -= _ => StopAim();
        }
        
        private void StarAim()
        {
           virtualCamera.Priority += priority;
           aimCanvas.enabled = true;
        }
        
        private void StopAim()
        {
            virtualCamera.Priority -= priority;
            aimCanvas.enabled = false;
        }
    }
}
