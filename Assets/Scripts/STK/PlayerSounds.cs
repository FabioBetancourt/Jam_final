using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public CharacterController controller;
    public Transform ground;

    public AudioSource pasos;
    private bool Hactivo;
    private bool Vactivo;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetButtonDown("Horizontal"))
        {
            if (!Hactivo)
            {
                Hactivo = true;
                pasos.Play();
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            Hactivo = false;

            if (!Vactivo)
            {
                pasos.Pause();
            }
        }

        if (Input.GetButtonDown("Vertical"))
        {
            if (!Vactivo)
            {
                Vactivo = true;
                pasos.Play();
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            Vactivo = false;

            if (!Hactivo)
            {
                pasos.Pause();
            }
        }
    }
}