using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int attackValue;

    [SerializeField]
    private int defenseValue;

    [Header("---UI---")]
    [SerializeField]
    private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {

    }

    private void UpdateVisuals()
    {
        healthSlider.value = (currentHealth / maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);//keep within bounds

        UpdateVisuals();
    }


}
