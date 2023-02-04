using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MutationBaker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    Upgrade upgrade;


    public void Bake(Upgrade toBake)
    {
        upgrade = toBake;
        if (upgrade == null) { return; }
        nameText.text = toBake.upgradeName;
        descriptionText.text = toBake.description;
    }

    public void UpgradeThis()
    {
        UpgradeManager.Instance.onUpgrade.Invoke(upgrade);
        PausingManager.Instance.UnPauseGame();
    }
}
