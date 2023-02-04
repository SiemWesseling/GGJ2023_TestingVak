using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Upgradebehaviours;
public class UpgradeManager : MonoBehaviour
{
    #region Instance
    private static UpgradeManager instance;
    public static UpgradeManager Instance { get { return instance; } private set { instance = value; } }

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
        foreach (PlayerStats.StatUpgrade up in upgrade.baseStatUpgrade)
        {
            PlayerStats.Instance.UpgradeValue(up);
        }
        var player = GameManager.Instance.player;


        //Handle adding new behaviour to the player
        UpgradeBehaviour[] currentVersions = player.GetComponents<UpgradeBehaviour>();
        bool versionFound = false;
        for (int i = 0; i < currentVersions.Length; i++)
        {
            Debug.Log(currentVersions[i].GetType() + " and " + upgrade.addThisToPlayer.GetType());
            if (currentVersions[i].GetType() == upgrade.addThisToPlayer.GetType())
            {
                versionFound = true;
                currentVersions[i].LevelUp();
            }
        }

        if (!versionFound && upgrade.addThisToPlayer)
        {
            //Todo: Check that this works
            GameManager.Instance.player.AddComponent(upgrade.addThisToPlayer.GetType());
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
}