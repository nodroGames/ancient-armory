using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RpgDB;

namespace AncientArmory
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class MercController : MonoBehaviour
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private int maxHealth = 0;

        [SerializeField]
        private int currentHealth = 0;

        Weapon weapon;
        public int defense;
        Character myCharacter;

        [SerializeField]
        private HealthUIController healthController;

        //member Components
        private Animator myAnimator;
        private SpriteRenderer mySpriteRenderer;

        // Start is called before the first frame update
        public void Start()
        {
            GatherReferences();
        }

        // Update is called once per frame
        void Update()
        {
            //healthController.UpdateHealth(currentHealth, maxHealth);
        }

        private void GatherReferences()
        {
            myCharacter = gameObject.GetComponent<RpgDB.Character>();
            myAnimator = GetComponent<Animator>() as Animator;
            mySpriteRenderer = GetComponent<SpriteRenderer>() as SpriteRenderer;
        }

        public void SetHealth()
        {
            maxHealth = myCharacter.Hit_Points();
            currentHealth = maxHealth;
            healthController.UpdateHealth(currentHealth, maxHealth);
        }

        public void SetArmor(Armor armor)
        {
            // Set armor for KAC check
            myCharacter.Armor = armor;
            defense = myCharacter.KAC();
        }

        private void Die()
        {

        }

        public void ApplyHealing(int healingAmount)
        {
            currentHealth += healingAmount;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            healthController.UpdateHealth(currentHealth, maxHealth);
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
                Die();
            else
                healthController.UpdateHealth(currentHealth, maxHealth);
        }

        public int Attack(MercController target)
        {
            bool hit = myCharacter.AttackCheck(weapon, target.defense);
            if (hit)
                return myCharacter.Attack(weapon, target.defense);
            return 0;
        }

        // This may be used in the future
        int resolveSingleOrDualWeildAttack(MercController attacker, int defense)
        {
            int damage = 0;
            if (attacker.Right_Hand.Name != "None")
                damage = attacker.Attack(attacker.Right_Hand, defense);
            if (attacker.Left_Hand.Name != "None" || attacker.Right_Hand.Name == "None")
                damage = attacker.Attack(attacker.Left_Hand, defense);
            return damage;
        }

    }
}