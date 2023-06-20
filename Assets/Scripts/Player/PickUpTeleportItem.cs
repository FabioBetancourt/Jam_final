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
            Debug.Log("Recolection action triggered"); 
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
        // Encuentra todos los objetos con la etiqueta "Egg" en la escena.
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");

        GameObject closestEgg = null;
        float closestDistance = Mathf.Infinity; // Establece la distancia más cercana en infinito para iniciar la búsqueda.

        // Itera sobre todos los objetos con la etiqueta "Egg".
        foreach (GameObject egg in eggs)
        {
            float distance = Vector3.Distance(transform.position, egg.transform.position); // Calcula la distancia desde el jugador hasta el "Egg".

            // Si la distancia calculada es menor que la distancia más cercana actual, actualiza la distancia más cercana y el "Egg" más cercano.
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEgg = egg;
            }
        }

        // Devuelve el "Egg" más cercano.
        return closestEgg;
    }


}
