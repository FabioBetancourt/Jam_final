using Player;
using UnityEngine;

namespace Eggs
{
    public class EggTeleporter : MonoBehaviour
    {
     
        public void Teleport()
        {
            Basket basket = GameObject.FindGameObjectWithTag("Basket").GetComponent<Basket>();
            transform.SetParent(basket.transform);
            transform.localPosition = basket.GetNextEggPosition();
            basket.AddEgg();
        }

    }

}
