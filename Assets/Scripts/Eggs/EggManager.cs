using System.Collections.Generic;
using UnityEngine;

namespace Eggs
{
    public class EggManager : MonoBehaviour
    {
        [SerializeField] private GameObject eggPrefab; // Prefab del huevo
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>(); // Puntos de spawn
        private List<GameObject> eggs = new List<GameObject>(); // Lista para mantener un registro de los huevos

        private void Start()
        {
            SpawnEggs();
        }

        private void Update()
        {
            CheckEggCollection();
        }

        // Spawnea los huevos en los puntos de spawn
        private void SpawnEggs()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                var egg = Instantiate(eggPrefab, spawnPoint.position, Quaternion.identity);
                eggs.Add(egg);
            }
        }

        // Comprueba si todos los huevos han sido recolectados
        private void CheckEggCollection()
        {
            foreach (var egg in eggs)
            {
                if (egg != null) return; // Si todavía hay huevos por recoger, no hagas nada
            }
        
            // Si todos los huevos han sido recolectados, respawnealos
            SpawnEggs();
        }
    }
}