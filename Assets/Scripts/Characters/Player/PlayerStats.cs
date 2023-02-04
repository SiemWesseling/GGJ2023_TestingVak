using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum upgrades
    {
        Speed,
        FireRate,
        ProjectileSpeed
    }

    public upgrades upgrade;

    private static PlayerStats instance;

    public static PlayerStats Instance { get { return instance; } private set { instance = value; } }
    private void Start()
    {
        if (!instance) { instance = this; }
        else { Debug.LogWarning("There seem to be multiple playerstats in the scene"); }
    }

    public void UpgradeValue(int upgrade)
    {
        Debug.Log("DoUpgrade");
        switch (upgrade)
        {
            case (int)upgrades.Speed: speedUpgrades++; break;

            case (int)upgrades.FireRate: fireRateUpgrades++; break;

            case (int)upgrades.ProjectileSpeed: projectileSpeedUpgrades++; break;
        }
    }
    #region UpgradeValues
    //this shouldn't be serialized, that's for testing
    [SerializeField] int speedUpgrades;
    float speedIncreasePerUpgrade = .1f;

    public float speedMult { get { return 1f + speedIncreasePerUpgrade * speedUpgrades; } }


    [SerializeField] int fireRateUpgrades;
    float fireRateIncreasePerUpgrade = .1f;
    public float fireRateMult { get { return 1f + fireRateIncreasePerUpgrade * fireRateUpgrades; } }

    [SerializeField] float projectileSpeedUpgrades;
    float projectileSpeedIncreasePerUpgrade = .1f;
    public float ProjectileSpeedMult { get { return 1f + projectileSpeedIncreasePerUpgrade * projectileSpeedUpgrades; } }
    #endregion
}
