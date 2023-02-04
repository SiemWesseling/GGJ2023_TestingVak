using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Behaviour[] _behavioursToDisableOnDeath;

    [SerializeField] float startHealth = 100;
    float maxHealth;
    public float currentHealth { get; private set; }
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        currentHealth = startHealth;
        maxHealth = startHealth;
        onLostHealth.AddListener(LoseHealth);
        onGainedHealth.AddListener(GainHealth);
        UpdateHealth();
    }

    public FloatEvent onLostHealth = new FloatEvent();
    public FloatEvent onGainedHealth = new FloatEvent();

    /// <summary>
    /// Parameters: Current health, Max health. 
    /// </summary>
    public FloatFloatEvent onHealthChanged = new FloatFloatEvent();
    [HideInInspector] public UnityEvent onDeath = new UnityEvent();

    /// <summary>
    /// Don't call this, rather the event related to it. 
    /// </summary>
    void LoseHealth(float amountLost)
    {
        currentHealth -= amountLost;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealth();
    }
    /// <summary>
    /// Don't call this, rather the event related to it. 
    /// </summary>
    void GainHealth(float amountGained)
    {
        currentHealth += amountGained;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
        UpdateHealth();
    }


    protected virtual void Die()
    {
        if (gameObject.tag != "Player")
        {
            animator.SetTrigger("enemyDies");
            foreach(Behaviour behaviour in _behavioursToDisableOnDeath)
            {
                behaviour.enabled = false;
            }
            
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    //This function is being ran by the animator
    private void RunAfterDeathAnim()
    {
        Debug.Log("Enemy died");
        onDeath.Invoke();
        Destroy(gameObject);
    }

    void UpdateHealth()
    {
        onHealthChanged.Invoke(currentHealth, maxHealth);
    }
}
