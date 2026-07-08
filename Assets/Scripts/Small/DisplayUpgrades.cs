using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUpgrades : MonoBehaviour
{
    TextMeshProUGUI UpgradeText;
    UpgradeSystem myUpgradeSystem;

    [SerializeField] int Upgrades = 0;
    void Start()
    {
        myUpgradeSystem = FindAnyObjectByType<UpgradeSystem>();
        UpgradeText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {       
        if(Upgrades == 0)
        {
            UpgradeText.text = myUpgradeSystem.GetSpeedDisplay().ToString();
        }

        if(Upgrades == 1)
        {
            UpgradeText.text = myUpgradeSystem.GetHealthDisplay().ToString();
        }

        if(Upgrades == 2)
        {
            UpgradeText.text = myUpgradeSystem.GetDamageDisplay().ToString();
        }

        if(Upgrades == 3)
        {
            UpgradeText.text = myUpgradeSystem.GetBoostTimeDisplay().ToString();
        }

        if(Upgrades == 4)
        {
            UpgradeText.text = myUpgradeSystem.GetDashDisplay().ToString();
        }
        if(Upgrades == 5)
        {
            UpgradeText.text = myUpgradeSystem.GetBombDisplay().ToString();
        }
        if(Upgrades == 6)
        {
            UpgradeText.text = myUpgradeSystem.GetRegenerationDisplay().ToString();
        }
    }
}
