using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AncientArmory
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class MercController : MonoBehaviour
    {
        [SerializeField]
        private string mercName;

        [SerializeField]
        private int maxHealth = 100;

        [SerializeField]
        private int currentHealth = 100;

        [Header("---Battle Stats---")]
        [SerializeField]
        private int attackValue = 2;

        [SerializeField]
        private int defenseValue = 1;

        [Header("---UI---")]
        [SerializeField]
        private HealthUIController healthController;

        //member Components
        private Animator myAnimator;
        private SpriteRenderer mySpriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            maxHealth = gameObject.GetComponent<RpgDB.Character>.Hit_Points();
            currentHealth = maxHealth;
            GatherReferences();

            healthController.UpdateHealth(currentHealth, maxHealth);
        }

        // Update is called once per frame
        void Update()
        {

            //healthController.UpdateHealth(currentHealth, maxHealth);
        }

        private void GatherReferences()
        {
            myAnimator = GetComponent<Animator>() as Animator;
            mySpriteRenderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        private void Die()
        {

        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);//keep within bounds

            healthController.UpdateHealth(currentHealth, maxHealth);
        }

    }
}