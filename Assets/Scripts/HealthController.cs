using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AncientArmory
{
    public class HealthController : MonoBehaviour
    {
        [Header("---UI Elements---")]
        [SerializeField]
        private Slider healthSlider;

        [Header("---Health Colors---")]
        [SerializeField]
        private HealthColors healthColors;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            healthSlider.value = currentHealth / maxHealth;
            healthColors.SetHealthColor(currentHealth, maxHealth, healthSlider);
        }
    }

}