using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AncientArmory
{
    public class HealthController : MonoBehaviour
    {
        [Header("---UI Elements---")]
        [SerializeField]
        private Slider healthSlider;

        [SerializeField]
        private Image healthSliderFill;

        [SerializeField]
        private TextMeshProUGUI healthTMP;

        [Header("---Health Colors---")]
        [SerializeField]
        private HealthColors healthColors;
        
        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            var healthPercent = currentHealth / maxHealth;

            if (healthSlider) healthSlider.value = healthPercent;
            if (healthSliderFill) healthSliderFill.color = healthColors.GetColor(healthPercent);
            if (healthTMP)
            {
                healthColors.SetHealthColor(currentHealth, maxHealth, healthTMP);
                //improve readout current / max
            }
        }
    }
}