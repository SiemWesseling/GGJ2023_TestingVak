using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;
public class HealthManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Behaviour[] _behavioursToDisableOnDeath;
    [SerializeField] private PlayerDiesAnim playerDiesAnim;
    [SerializeField] private string deathSoundName;

    [SerializeField] public float startHealth = 100;
    public float maxHealth;

    private TimerUI RoboRally;
    public float currentHealth { get; private set; }

    private int hitsTaken;
    private void Start()
    {
        //TestingConnect.AnalitycsInitializedSucces += OnAnalyticsInitializedSucces;

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
    /// 

    private void OnAnalyticsInitializedSucces()
    {
        
        // Now you can log events to the Analytics service
        AnalyticsService.Instance.CustomData("PlayerGetsHit", new Dictionary<string, object> {
            { "PlayerHit", hitsTaken }
        });
        

        AnalyticsService.Instance.CustomData("SurvivalTime", new Dictionary<string, object> {
            { "TimeSurvived", RoboRally.timer }
        });

        AnalyticsService.Instance.Flush(); //testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo//testing is a big fat doodoo
    }

    void LoseHealth(float amountLost)
    {
        currentHealth -= amountLost;
        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;
            OnAnalyticsInitializedSucces();
        }

        if(gameObject.tag == "Player")
        {
            hitsTaken++;
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
            foreach (Behaviour behaviour in _behavioursToDisableOnDeath)
            {
                behaviour.enabled = false;
            }

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            if (playerDiesAnim != null)
            {
                playerDiesAnim.StartDeathAnim();
                foreach (Behaviour behaviour in _behavioursToDisableOnDeath)
                {
                    behaviour.enabled = false;
                }

                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

        }

        AudioManager.Instance?.PlaySound(deathSoundName);
    }

    //This function is being ran by the animator
    private void RunAfterDeathAnim()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }

    void UpdateHealth()
    {
        onHealthChanged.Invoke(currentHealth, maxHealth);
    }
    private void OnDestroy()
    {
        onLostHealth.RemoveListener(LoseHealth);
        onGainedHealth.RemoveListener(GainHealth);
    }
}
