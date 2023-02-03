using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float startHealth = 100;
    float maxHealth;
    float currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
        maxHealth = startHealth;
        onLostHealth.AddListener(LoseHealth);
        onDeath.AddListener(Die);
        onGainedHealth.AddListener(GainHealth);
    }

    public FloatEvent onLostHealth = new FloatEvent();
    public FloatEvent onGainedHealth = new FloatEvent();
    public UnityEvent onDeath = new UnityEvent();

    /// <summary>
    /// Don't call this, rather the event related to it. 
    /// </summary>
    void LoseHealth(float amountLost)
    {
        currentHealth -= amountLost;
        if (currentHealth < 0)
        {
            onDeath.Invoke();
        }
    }
    /// <summary>
    /// Don't call this, rather the event related to it. 
    /// </summary>
    void GainHealth(float amountGained)
    {
        currentHealth += amountGained;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
    }


    protected virtual void Die()
    {

    }
}
