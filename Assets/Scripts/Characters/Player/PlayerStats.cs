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

    private static PlayerStats instance;

    public static PlayerStats Instance { get { return instance; } private set { instance = value; } }
    private void Start()
    {
        if (!instance) { instance = this; }
        else { Debug.LogWarning("There seem to be multiple playerstats in the scene"); }
    }

    void UpgradeValue(upgrades upgrade)
    {
        switch (upgrade)
        {
            case upgrades.Speed: speedUpgrades++; break;

            case upgrades.ProjectileSpeed: projectileSpeedUpgrades++; break;

            case upgrades.FireRate: fireRateUpgrades++; break;
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

    float projectileSpeedUpgrades;
    float projectileSpeedIncreasePerUpgrade = .1f;
    public float ProjectileSpeedMult { get { return 1f + projectileSpeedIncreasePerUpgrade * projectileSpeedUpgrades; } }
    #endregion
}
