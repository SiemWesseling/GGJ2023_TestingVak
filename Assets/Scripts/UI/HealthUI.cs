using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
     Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthManager.onHealthChanged.AddListener(ChangeHealth);
    }

    void ChangeHealth(float currentHealth, float maxHealth)
    {
        Debug.Log(" Pasta");
        healthSlider.value = currentHealth / maxHealth;
    }
}
