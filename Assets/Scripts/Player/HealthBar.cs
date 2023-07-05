using UnityEngine;
using UnityEngine.UI;
namespace Player
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void UpdateHealthBar(float health)
        {
            slider.value = health;
        }
    }

}
