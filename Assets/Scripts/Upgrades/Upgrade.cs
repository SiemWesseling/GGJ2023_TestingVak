using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Upgradebehaviours;
using System.Runtime.CompilerServices;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] public string upgradeName;
    [SerializeField] public string description;
    [Tooltip("If you want to add more you have to add new possibilities to the enum")]
    [SerializeField] private UpgradeBehavioursData.behaviours addThis;
    [SerializeField]
    public UpgradeBehaviour addThisToPlayer
    {
        get
        {
            Debug.Log(new ShootBackwards());
            return UpgradeBehavioursData.GetBehaviour(addThis);
        }
    }
    [SerializeField] public PlayerStats.StatUpgrade[] baseStatUpgrade;
}

[System.Serializable]
class UpgradeBehavioursData
{
    public enum behaviours
    {
        Nothing,
        ShootBack
    }

    public static UpgradeBehaviour GetBehaviour(behaviours value)
    {
        if (value == behaviours.ShootBack)
        {
            return new ShootBackwards();
        }
        return null;
    }
}