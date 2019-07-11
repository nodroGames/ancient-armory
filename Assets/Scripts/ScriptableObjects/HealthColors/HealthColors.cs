using UnityEngine;
using TMPro;

namespace AncientArmory
{
    [CreateAssetMenu(fileName = "HealthColors_", menuName = "ScriptableObjects/New Health Colors")]
    public class HealthColors : ScriptableObject
    {
        [Header("Colors")]
        [SerializeField]
        private  Color color_HealthHigh;
        
        [SerializeField]
        private  Color color_HealthLow;

        [SerializeField]
        private Color color_HealthDanger;

        [Header("Limits")]
        [SerializeField]
        private float limit_HealthLow = .25f;

        [SerializeField]
        private float limit_HealthDanger = .10f;

        public void SetHealthColor(int currentHealth, int maxHealth, TextMeshProUGUI healthText)
        {
            var healthPercent = currentHealth / maxHealth; //get percent
                                                           //Debug.Log("healthPercent: " + healthPercent.ToString());//print test
            if (healthPercent <= limit_HealthDanger)
            {
                healthText.color = color_HealthDanger;
            }
            else if (healthPercent < limit_HealthLow)
            {
                healthText.color = color_HealthLow;
            }
            else
            {
                healthText.color = color_HealthHigh;
                healthText.text = currentHealth.ToString();
                //TODO get a color between white and yellow this percent
            }
        }

        public Color GetColor(float healthPercent)
        {
            if(healthPercent > limit_HealthLow)
            {
                return color_HealthHigh;
            }
            else if(healthPercent > limit_HealthDanger)
            {
                return color_HealthLow;
            }
            else
            {
                return color_HealthDanger;
            }
        }
    }
}
