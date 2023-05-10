using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Upgradebehaviours;
public class UpgradeManager : MonoBehaviour
{
    #region Instance
    private static UpgradeManager instance;
    public static UpgradeManager Instance { get { return instance; } private set { instance = value; } }

    private int amountOfUpgrades;
    private void Start()
    {
        instance = this;
        mutationBakers = GetComponentsInChildren<MutationBaker>();
        onUpgrade.AddListener(ExecuteUpgrade);
        onLevelUp.AddListener(GenerateNewUpgrades);
        gameObject.active = false;
    }

    #endregion

    [HideInInspector] public UnityEvent onLevelUp = new UnityEvent();

    public UpgradeEvent onUpgrade = new UpgradeEvent();
    [SerializeField] List<Upgrade> availableUpgrades;

    MutationBaker[] mutationBakers;

    void ExecuteUpgrade(Upgrade upgrade)
    {
        //Manage the base stat upgrades
        foreach (PlayerStats.StatUpgrade up in upgrade.baseStatUpgrade)
        {
            PlayerStats.Instance.UpgradeValue(up);
        }
        //Handle adding new behaviour to the player
        ApplyAddedComponent(upgrade.addThis);

        amountOfUpgrades++;
        OnAnalyticsInitializedSucces();

    }

    void ApplyAddedComponent(UpgradeBehavioursData.behaviours behaviour)
    {
        var player = GameManager.Instance.player;
        switch (behaviour)
        {
            case UpgradeBehavioursData.behaviours.Nothing:
                return;
                break;

            case UpgradeBehavioursData.behaviours.ShootBack:
                ShootBackwards currentVersion = player.GetComponent<ShootBackwards>();
                if (currentVersion) { currentVersion.LevelUp(); }
                else { player.AddComponent<ShootBackwards>(); }
                break;

        }
    }

    void GenerateNewUpgrades()
    {
        List<Upgrade> remainingUpgrades = new List<Upgrade>(availableUpgrades);//Copies the availableUpgrades list
        for (int i = 0; i < mutationBakers.Length; i++)
        {
            int newUpgrade = Random.Range(0, remainingUpgrades.Count);
            mutationBakers[i].Bake(remainingUpgrades[newUpgrade]);
            remainingUpgrades.RemoveAt(newUpgrade);
        }
    }
    private void OnAnalyticsInitializedSucces()
    {
        //// Unsubscribe from the event
        //TestingConnect.AnalitycsInitializedSucces -= OnAnalyticsInitializedSucces;
        Debug.Log("Sending getshit event");

        // Now you can log events to the Analytics service
        AnalyticsService.Instance.CustomData("amountOfUpgradesGotten", new Dictionary<string, object> {
            { "SceneName", SceneManager.GetActiveScene().name },
            { "amountOfLevels", amountOfUpgrades }
        });
        AnalyticsService.Instance.Flush();
    }
}