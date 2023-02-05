using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum StatUpgrade
    {
        Speed,
        FireRate,
        ProjectileSpeed,
        ProjectileSize,
        BulletDamage,
    }

    private static PlayerStats instance;

    public static PlayerStats Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {
        if (!instance) { instance = this; }
        else { Debug.LogWarning("There seem to be multiple playerstats in the scene"); }
    }


    public void UpgradeValue(StatUpgrade upgrade) { UpgradeValue((int)upgrade); }

    public void UpgradeValue(int upgrade)
    {
        switch (upgrade)
        {
            case (int)StatUpgrade.Speed: speedUpgrades++; break;

            case (int)StatUpgrade.FireRate: fireRateUpgrades++; break;

            case (int)StatUpgrade.ProjectileSpeed: projectileSpeedUpgrades++; break;
            
            case (int)StatUpgrade.BulletDamage: bulletDamage++; break;

            case (int)StatUpgrade.ProjectileSize: projectileSizeUpgrades++; break;
        }
    }

    #region UpgradeValues
    //this shouldn't be serialized, that's for testing
    [SerializeField] int speedUpgrades;
    float speedIncreasePerUpgrade = .2f;

    public float speedMult { get { return 1f + speedIncreasePerUpgrade * speedUpgrades; } }


    [SerializeField] int fireRateUpgrades;
    float fireRateIncreasePerUpgrade = .2f;
    public float fireRateMult { get { return 1f + fireRateIncreasePerUpgrade * fireRateUpgrades; } }

    [SerializeField] float projectileSpeedUpgrades;
    float projectileSpeedIncreasePerUpgrade = .2f;
    public float ProjectileSpeedMult { get { return 1f + projectileSpeedIncreasePerUpgrade * projectileSpeedUpgrades; } }

    [SerializeField] float projectileSizeUpgrades;
    float projectileSizeIncreasePerUpgrade = .2f;
    public float ProjectileSizeMult { get { return 1f + projectileSizeIncreasePerUpgrade * projectileSizeUpgrades; } }

    [SerializeField] public float bulletDamage;

    #endregion
}
