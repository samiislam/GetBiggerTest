using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    
    public float runSpeed = 25f;
    public int showSpeed = 1;
    public float health = 1;
    public int showHealth = 1;
    public float damage = 1;
    public int showDamage = 1;
    public float boostTime = 5f;
    public int showBoostTime = 1;
    public float dashSpeed = 50f;
    public int showDash = 1;
    public float bombDamage = 5f;
    public int showBombs = 1;
    public float regeneration = 2f;
    public int showRegeneration = 1;
    public float coolDownRegeneration = 60f;
    public float coolDownBombs = 60f;
    public float coolDownDash = 60f;
    public float money = 0;
    public float moneyPlus = 0;
    public bool canDash = false;
    public bool canPlaceBombs = false;
    public bool canRegenerate = false;

    public int finishedLevel = 0;
    public int finishedObstacleLevel = 0;

    public float upgradeCostHealth = 10f;
    public float upgradeCostDamage = 10f;
    public float upgradeCostSpeed = 10f;
    public float upgradeCostBoostTime = 10f;
    public float upgradeCostDash = 10f;
    public float upgradeCostRegeneration = 10f;
    public float upgradeCostBombs = 10f;
    
    public bool healthMax = false;
    public bool damageMax = false;
    public bool speedMax = false;
    public bool boostTimeMax = false;
    public bool dashMax = false;
    public bool bombsMax = false;
    public bool regenerationMax = false;
    Player player;

    private static readonly string settingsFileName = "upgradeSettings.upgs";
    
    #region SaveLoad Upgrade Settings

    static void LoadUpgradeSettings()
    {
        string path = $"{Application.persistentDataPath}/{settingsFileName}";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            using var stream = new FileStream(path, FileMode.Open);
            UpgradeData data = formatter.Deserialize(stream) as UpgradeData;

            if (instance)
            {
                instance.runSpeed = data.runSpeed;
                instance.showSpeed = data.showSpeed;
                instance.health = data.health;
                instance.showHealth = data.showHealth;
                instance.damage = data.damage;
                instance.showDamage = data.showDamage;
                instance.boostTime = data.boostTime;
                instance.showBoostTime = data.showBoostTime;
                instance.dashSpeed = data.dashSpeed;
                instance.showDash = data.showDash;
                instance.bombDamage = data.bombDamage;
                instance.showBombs = data.showBombs;
                instance.regeneration = data.regeneration;
                instance.showRegeneration = data.showRegeneration;
                instance.coolDownRegeneration = data.coolDownRegeneration;
                instance.coolDownBombs = data.coolDownBombs;
                instance.coolDownDash = data.coolDownDash;
                instance.money = data.money;
                instance.moneyPlus = data.moneyPlus;
                instance.canDash = data.canDash;
                instance.canPlaceBombs = data.canPlaceBombs;
                instance.canRegenerate = data.canRegenerate;

                instance.finishedLevel = data.finishedLevel;
                instance.finishedObstacleLevel = data.finishedObstacleLevel;

                instance.upgradeCostHealth = data.upgradeCostHealth;
                instance.upgradeCostDamage = data.upgradeCostDamage;
                instance.upgradeCostSpeed = data.upgradeCostSpeed;
                instance.upgradeCostBoostTime = data.upgradeCostBoostTime;
                instance.upgradeCostDash = data.upgradeCostDash;
                instance.upgradeCostRegeneration = data.upgradeCostRegeneration;
                instance.upgradeCostBombs = data.upgradeCostBombs;
                instance.healthMax = data.healthMax;
                instance.damageMax = data.damageMax;
                instance.speedMax = data.speedMax;
                instance.boostTimeMax = data.boostTimeMax;
                instance.dashMax = data.dashMax;
                instance.bombsMax = data.bombsMax;
                instance.regenerationMax = data.regenerationMax;
                Debug.Log($"Loaded upgrade settings from {path}");
            }
        }
    }

    public static void SaveUpgradeSettings()
    {
        if(instance == null)
        {
            Debug.Log("Could not save UpgradeSettings");
            return;
        }
        
        string path = $"{Application.persistentDataPath}/{settingsFileName}";
        BinaryFormatter formatter = new BinaryFormatter();
        using var stream = new FileStream(path, FileMode.Create);
        UpgradeData data = new UpgradeData(instance);
        formatter.Serialize(stream, data);
        Debug.Log($"Saved upgrade settings to {path}");
    }

    #endregion

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
            LoadUpgradeSettings();
            Application.quitting += SaveUpgradeSettings;
        }

        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        player = FindAnyObjectByType<Player>();
    }
}
