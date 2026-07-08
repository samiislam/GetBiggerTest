using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> newDash = new List<GameObject>();
    [SerializeField] List<GameObject> newBombs = new List<GameObject>();
    [SerializeField] List<GameObject> newRegeneration = new List<GameObject>();

    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    
    void Update()
    {
        if (UpgradeManager.instance == null)
        {
            return;
        }
        
        if(UpgradeManager.instance.canDash == true)
        {
            foreach (GameObject obj in newDash)
            {
                UpgradeManager.SaveUpgradeSettings();
                obj.SetActive(true);
            }
        }

        if(UpgradeManager.instance.canPlaceBombs == true)
        {
            foreach (GameObject obj in newBombs)
            {
                UpgradeManager.SaveUpgradeSettings();
                obj.SetActive(true);
            }
        }

        if(UpgradeManager.instance.canRegenerate == true)
        {
            foreach (GameObject obj in newRegeneration)
            {
                UpgradeManager.SaveUpgradeSettings();
                obj.SetActive(true);
            }
        }
        
        if (player != null)
        {
            UpgradeManager.instance.moneyPlus = player.GetMoney();
        }
    }

    public void UpdateMoney()
    {
        if (UpgradeManager.instance == null)
        {
            return;
        }
        
        UpgradeManager.instance.money += UpgradeManager.instance.moneyPlus;
    }

    public void UpgradeHealth()
    {
        if(UpgradeManager.instance.showHealth < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.health += 1;
            UpgradeManager.instance.showHealth += 1;
        }
        else
        {
            UpgradeManager.instance.healthMax = true;
        } 
    }

    public void UpgradeDamage()
    {
        if(UpgradeManager.instance.showDamage < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.damage += 1;
            UpgradeManager.instance.showDamage += 1;
        }      
        else
        {
            UpgradeManager.instance.damageMax = true;
        } 
    }

    public void UpgradeBoostTime()
    {
        if(UpgradeManager.instance.showBoostTime < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.boostTime += 1;
            UpgradeManager.instance.showBoostTime += 1;
        }
        else
        {
            UpgradeManager.instance.boostTimeMax = true;
        }
    }

    public void UpgradeRunSpeed()
    {
        if(UpgradeManager.instance.showSpeed < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.runSpeed -= 0.1f;
            UpgradeManager.instance.showSpeed += 1;
        }
        else
        {
            UpgradeManager.instance.speedMax = true;
        }     
    }

    public void UpgradeDash()
    {
        if(UpgradeManager.instance.showDash < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.dashSpeed += 1;
            UpgradeManager.instance.coolDownDash -= 2;
            UpgradeManager.instance.showDash += 1;
        }
        else
        {
            UpgradeManager.instance.dashMax = true;
        } 
    }

    public void UpgradeBombs()
    {
        if(UpgradeManager.instance.showBombs < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.bombDamage += 1;
            UpgradeManager.instance.coolDownBombs -= 2;
            UpgradeManager.instance.showBombs += 1;
        }
        else
        {
            UpgradeManager.instance.bombsMax = true;
        } 
    }

    public void UpgradeRegeneration()
    {
        if(UpgradeManager.instance.showRegeneration < 10)
        {
            UpgradeManager.SaveUpgradeSettings();
            UpgradeManager.instance.regeneration += 1;
            UpgradeManager.instance.coolDownRegeneration -= 2;
            UpgradeManager.instance.showRegeneration += 1;
        }
        else
        {
            UpgradeManager.instance.regenerationMax = true;
        } 
    }

    public void UnlockDash()
    {
        UpgradeManager.instance.canDash = true;
    }

    public void UnlockBombs()
    {
        UpgradeManager.instance.canPlaceBombs = true;
    }

    public void UnlockRegeneration()
    {
        UpgradeManager.instance.canRegenerate = true;
    }

    public bool GetDash()
    {
        return UpgradeManager.instance.canDash;
    }

    public float GetDashSpeed()
    {
        return UpgradeManager.instance.dashSpeed;
    }

    public float GetDashCoolDown()
    {
        return UpgradeManager.instance.coolDownDash;
    }

    public float GetDashDisplay()
    {
        return UpgradeManager.instance.showDash;
    }

    public bool GetBombs()
    {
        return UpgradeManager.instance.canPlaceBombs;
    }

    public float GetBombDamage()
    {
        return UpgradeManager.instance.bombDamage;
    }

    public float GetBombCoolDown()
    {
        return UpgradeManager.instance.coolDownBombs;
    }

    public float GetBombDisplay()
    {
        return UpgradeManager.instance.showBombs;
    }

    public bool GetRegenerate()
    {
        return UpgradeManager.instance.canRegenerate;
    }

    public float GetRegenerationAmount()
    {
        return UpgradeManager.instance.regeneration;
    }

    public float GetRegenerationCoolDown()
    {
        return UpgradeManager.instance.coolDownRegeneration;
    }

    public float GetRegenerationDisplay()
    {
        return UpgradeManager.instance.showRegeneration;
    }

    public float GetRunSpeed()
    {
        return UpgradeManager.instance.runSpeed;
    }

    public float GetSpeedDisplay()
    {
        return UpgradeManager.instance.showSpeed;
    }

    public float GetHealth()
    {
        return UpgradeManager.instance.health;
    }

    public float GetHealthDisplay()
    {
        return UpgradeManager.instance.showHealth;
    }

    public float GetDamage()
    {
        return UpgradeManager.instance.damage;
    }

    public float GetDamageDisplay()
    {
        return UpgradeManager.instance.showDamage;
    }

    public float GetBoostTime()
    {
        return UpgradeManager.instance.boostTime;
    }
    
    public float GetBoostTimeDisplay()
    {
        return UpgradeManager.instance.showBoostTime;
    }
}