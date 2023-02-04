using UnityEngine;

public class HealthRegain : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    private bool canRegen = false;
    
    [SerializeField] private float regenRate = 1;
    private float regenningTimer = 1;
    
    
    
    [SerializeField] private float regenWait = 5;
    private float waitTimer = 5;
    
    private void Awake()
    {
        if (healthManager == null)
        {
            healthManager = GetComponent<HealthManager>();
        }
        
        healthManager.onLostHealth.AddListener(LostHealth);
    }
    
    private void LostHealth(float damage)
    {
        canRegen = false;
        waitTimer = 0;
    }

    private void FixedUpdate()
    {
        if ((int)healthManager.currentHealth == (int)healthManager.maxHealth)
        {
            return;
        }
        
        if (!canRegen)
        {
            WaitToRegen();
            return;
        }
        
        Regen();
    }
    
    private void WaitToRegen()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= regenWait)
        {
            canRegen = true;
            waitTimer = 0;
        }
    }

    private void Regen()
    {
        regenningTimer += Time.deltaTime;
        if (regenningTimer >= regenRate)
        {
            healthManager.onGainedHealth.Invoke(1);
            regenningTimer = 0;
        }
    }

    private void OnDestroy()
    {
        healthManager.onLostHealth.RemoveListener(LostHealth);
    }
}
