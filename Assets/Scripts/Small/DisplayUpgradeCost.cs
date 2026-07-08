using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayUpgradeCost : MonoBehaviour
{
    [SerializeField] float index = 0;
    TextMeshProUGUI upgradeText;
    UpgradeCost myUpgradeCost;
    void Start()
    {
        myUpgradeCost = FindAnyObjectByType<UpgradeCost>();
        upgradeText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(index == 0 && UpgradeManager.instance.healthMax == false)
        {
            upgradeText.text = myUpgradeCost.GetHealthCost().ToString();
        }
        if(index == 0 && UpgradeManager.instance.healthMax == true)
        {
            upgradeText.text = "Max";
        }
        if(index == 1 && UpgradeManager.instance.damageMax == false)
        {
            upgradeText.text = myUpgradeCost.GetDamageCost().ToString();
        }
        if(index == 1 && UpgradeManager.instance.damageMax == true)
        {
            upgradeText.text = "Max";
        }
        if(index == 2 && UpgradeManager.instance.speedMax == false)
        {
            upgradeText.text = myUpgradeCost.GetSpeedCost().ToString();
        }
        if(index == 2 && UpgradeManager.instance.speedMax == true)
        {
            upgradeText.text = "Max";
        }
        if(index == 3 && UpgradeManager.instance.boostTimeMax == false)
        {
            upgradeText.text = myUpgradeCost.GetBoostTimeCost().ToString();
        }
        if(index == 3 && UpgradeManager.instance.boostTimeMax == true)
        {
            upgradeText.text = "Max";
        }
        if(index == 4 && UpgradeManager.instance.dashMax == false)
        {
            upgradeText.text = myUpgradeCost.GetDashCost().ToString();
        }
        if(index == 4 && UpgradeManager.instance.dashMax == true)
        {
            upgradeText.text = "Max";
        }
        if(index == 5 && UpgradeManager.instance.regenerationMax == false)
        {
            upgradeText.text = myUpgradeCost.GetRegenerationCost().ToString();
        }
        if(index == 5 && UpgradeManager.instance.regenerationMax == true)
        {
            upgradeText.text = "Max";
        }
        if(index == 6 && UpgradeManager.instance.bombsMax == false)
        {
            upgradeText.text = myUpgradeCost.GetBombsCost().ToString();
        }
        if(index == 6 && UpgradeManager.instance.bombsMax == true)
        {
            upgradeText.text = "Max";
        }
    }
}
