using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpTeleportItem : MonoBehaviour
{
    [Tooltip("Distance the teleport above the player")]
    public float teleportUpDistance = 1.8f;
    [Tooltip("Distance the teleport back the player")]
    public float teleportBackDistance = 0.8f;
    [Tooltip("Max distance to pick up an egg")]
    public float closestDistance = 2f; 
    private PlayerInput playerInput;
    private Transform basketTransform;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        basketTransform = transform.Find("Basket"); // Encuentra el objeto "Basket" dentro del jugador.
    }

    private void Update()
    {
        if(playerInput.actions["Recolection"].triggered)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        GameObject egg = GetClosestEgg(); // Encuentra el "Egg" más cercano.
        if(egg != null)
        {
            Rigidbody eggRigidbody = egg.GetComponent<Rigidbody>();
            if(eggRigidbody != null)
            {
                eggRigidbody.isKinematic = true;  // Desactiva las físicas para el teletransporte.
                egg.transform.position = playerRigidbody.position + Vector3.up * teleportUpDistance - playerRigidbody.transform.forward * teleportBackDistance;  // Teletransporta el "Egg" 2 unidades arriba de la cabeza del jugador y media unidad detrás.
                eggRigidbody.isKinematic = false;  // Reactiva las físicas para que el "Egg" caiga.
            }
        }
    }
    private GameObject GetClosestEgg()
    {
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");

        GameObject closestEgg = null;

        foreach (GameObject egg in eggs)
        {
            float distance = Vector3.Distance(transform.position, egg.transform.position);

            if (distance < closestDistance)
            {
                closestEgg = egg;
            }
        }
        return closestEgg;
    }



}
