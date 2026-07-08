using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float runSpeed = 20f;
    [SerializeField] float health = 1;
    [SerializeField] float maxHealth = 1;
    [SerializeField] float damage = 1;
    [SerializeField] float boostTime = 5f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float dashSpeed = 50f;
    [SerializeField] float bombDamage = 5f;
    [SerializeField] float regeneration = 5f;
    [SerializeField] float coolDownRegeneration = 2f;
    [SerializeField] float coolDownBombs = 2f;
    [SerializeField] float coolDownDash = 2f;
    [SerializeField] float money = 0;


    [Header("VFX, SFX")]
    [SerializeField] GameObject explodeVFX;
    [SerializeField] GameObject dashVFX;
    [SerializeField] GameObject damageBoostVFX;
    [SerializeField] GameObject iceBoostVFX;
    [SerializeField] GameObject speedShootVFX;
    [SerializeField] AudioClip regenerationSFX;
    [SerializeField] GameObject regenerationVFX;
    [SerializeField] GameObject healthMaxVFX;
    [SerializeField] GameObject effektPoint;
    [SerializeField] AudioClip dashSFX;
    [SerializeField] float dashVolume;
    [SerializeField] AudioClip bombPlaceSFX;
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip keySFX;
    [SerializeField] float explosionSFXVolume;
    
    [Header("Sprite")]
    [SerializeField] Sprite defaultPlayerSprite;
    [SerializeField] Sprite myDamageBoostSprite;
    [SerializeField] Sprite iceBoostSprite;
    [SerializeField] Sprite regenerationSprite;
    [SerializeField] Sprite healthMaxSprite;
    [SerializeField] Sprite playerHurtSprite;
    [SerializeField] Sprite shootSpeedSprite;
    
    [Header("Scene")]
    public Joystick joystickMove;
    public Joystick joystickAim;
    public GameObject aimPoint;
    [SerializeField] GameObject dashButton;
    [SerializeField] GameObject bombButton;
    [SerializeField] GameObject regenerationButton;
    [SerializeField] GameObject bomb;
    [SerializeField] int keys = 0; 
    [SerializeField] Camera cam;
    
    [Header("Time")]
    [SerializeField] float wait1 = 0.01f;
    [SerializeField] float wait2 = 0.01f;
    
    bool maxHealthSet = false;
    bool takeDamage = true;
    bool openDoor = false;
    bool regenerationCoolDown = true;
    bool bombsCoolDown = true;
    bool dashCoolDown = true;
    bool iceBoost = false;
    bool playerDestroyed = false;
    bool canDash;
    bool canPlaceBombs;
    bool canRegenerate;

    HealthBar healthBar;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidbody;
    Enemy myEnemy;
    GameObject myDamageBoostEffekt;
    GameObject myHealthMaxEffekt;
    GameObject myIceBoostVFX;
    GameObject mySpeedShootVfX;
    GameObject regenerationEffekt;
    KeysPerLevel myKeysPerLevel;
    UpgradeSystem myUpgradeSystem;
    Door myDoor;
    Shooting myShooting;


    void Start()
    {
        healthBar = FindAnyObjectByType<HealthBar>();
        myKeysPerLevel = FindAnyObjectByType<KeysPerLevel>();
        myEnemy = FindAnyObjectByType<Enemy>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myUpgradeSystem = FindAnyObjectByType<UpgradeSystem>();
        myDoor = FindAnyObjectByType<Door>();
        myShooting = GetComponent<Shooting>();

        canDash = myUpgradeSystem.GetDash();
        canPlaceBombs = myUpgradeSystem.GetBombs();
        canRegenerate = myUpgradeSystem.GetRegenerate();

        if(canDash == true)
        {
            dashButton.SetActive(true);
        }
        if(canPlaceBombs == true)
        {
            bombButton.SetActive(true);
        }
        if(canRegenerate == true)
        {
            regenerationButton.SetActive(true);
        }
        
        health = myUpgradeSystem.GetHealth();
        damage = myUpgradeSystem.GetDamage();
        boostTime = myUpgradeSystem.GetBoostTime();
        bombDamage = myUpgradeSystem.GetBombDamage();
        regeneration = myUpgradeSystem.GetRegenerationAmount();
        dashSpeed = myUpgradeSystem.GetDashSpeed();
        coolDownDash = myUpgradeSystem.GetDashCoolDown();
        coolDownBombs = myUpgradeSystem.GetBombCoolDown();
        coolDownRegeneration = myUpgradeSystem.GetRegenerationCoolDown();
    }

    void Update()
    {
        if (maxHealth < health)
        {
            health -= 1f;
        }
        if (!maxHealthSet)
        {
            maxHealth = health;
            int newMaxHealth = Convert.ToInt32(maxHealth);
            healthBar.SetMaxHealth(newMaxHealth);
            maxHealthSet = true;
        }

        if (myDamageBoostEffekt != null)
        {
            myDamageBoostEffekt.transform.position = effektPoint.transform.position;
        }
        if (myHealthMaxEffekt != null)
        {
            myHealthMaxEffekt.transform.position = effektPoint.transform.position;
        }

        if (regenerationEffekt != null)
        {
            regenerationEffekt.transform.position = transform.position;
        }

        if (myIceBoostVFX != null)
        {
            myIceBoostVFX.transform.position = transform.position;
        }
        if (mySpeedShootVfX != null)
        {
            mySpeedShootVfX.transform.position = transform.position;
        }
        
        Move();
        Aim();
        KeyCheck();
    }
    public void PlaceBomb() 
    {
        if(bombsCoolDown == true)
        {
            AudioSource.PlayClipAtPoint(bombPlaceSFX, Camera.main.transform.position);
            Instantiate(bomb, transform.position, Quaternion.identity);
            
            StartCoroutine(WaitPlaceBombs());
        }
        
    }
    IEnumerator WaitPlaceBombs()
    {
        bombsCoolDown = false;
        yield return new WaitForSeconds(coolDownBombs);
        bombsCoolDown = true;
    }
    public void Dash()
    {
        if(dashCoolDown == true)
        {
            StartCoroutine(DashCoroutine());
        } 
    }

    public void Regeneration()
    {
        if(regenerationCoolDown == true)
        {
            AudioSource.PlayClipAtPoint(regenerationSFX, Camera.main.transform.position);
            health += regeneration;
            int newHealth = Convert.ToInt32(health);
            healthBar.SetHealth(newHealth);
            
            regenerationEffekt = Instantiate(regenerationVFX, transform.position, Quaternion.identity);
            StartCoroutine(RegenerationSprite());
            StartCoroutine(WaitRegeneration());
        }
    }

    IEnumerator RegenerationSprite()
    {
        mySpriteRenderer.sprite = regenerationSprite;
        yield return new WaitForSeconds(.25f);
        mySpriteRenderer.sprite = defaultPlayerSprite;
    }
    IEnumerator WaitRegeneration()
    {
        regenerationCoolDown = false;
        yield return new WaitForSeconds(coolDownRegeneration);
        regenerationCoolDown = true;
    }

    IEnumerator DashCoroutine()
    {
        Instantiate(dashVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(dashSFX, Camera.main.transform.position, dashVolume);
        takeDamage = false;
        PlayerPrefs.SetFloat("LastRunSpeed", runSpeed);
        PlayerPrefs.Save(); 
        runSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        runSpeed = PlayerPrefs.GetFloat("LastRunSpeed"); 
        takeDamage = true;
        StartCoroutine(DashWaitCoroutine());
    }

    IEnumerator DashWaitCoroutine()
    {
        dashCoolDown = false;
        yield return new WaitForSeconds(coolDownDash);
        dashCoolDown = true;
    }

    private void Aim()
    {

        if (joystickAim.Direction != Vector2.zero)
        {
            var lookDir = joystickAim.Direction;
        
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            myRigidbody.rotation = angle - 180f;
        }
    }

    private void Move()
    {
        float ControlThrow = joystickMove.Horizontal;
        float ControlDown = joystickMove.Vertical;

        Vector2 playerVelocity = new Vector2(ControlThrow * runSpeed, ControlDown * runSpeed);
        
        myRigidbody.linearVelocity = playerVelocity;
    }

   /*  public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform Object)
    {
        float timer = 0;
        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (Object.transform.position - this.transform.position).normalized;
            myRigidbody.AddForce(-direction * knockbackPower);
        }
        yield return 0;
    } */

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag =="Wall")
        {
            StartCoroutine(DestroyPlayer());
            //health -= 1;
            //StartCoroutine(Hurt());
        }

        if  (collider2D.tag =="PickUp")
        {
            
            health += 1;
            AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position);
            int newHealth = Convert.ToInt32(health);
            healthBar.SetHealth(newHealth);
        }
        
        if (collider2D.tag == "Enemy" && takeDamage == true)
        {
            float hurt = myEnemy.GetDamage();
            health -= hurt;
            StartCoroutine(Hurt());
            int newHealth = Convert.ToInt32(health);
            healthBar.SetHealth(newHealth);
        }

        if (collider2D.tag == "EnemyBullet" && takeDamage == true)
        {
            float hurt = myEnemy.GetRangedDamage();
            health -= hurt;
            StartCoroutine(Hurt());
            int newHealth = Convert.ToInt32(health);
            healthBar.SetHealth(newHealth);
        }

        if (collider2D.tag =="DamageBoost")
        {
            StartCoroutine(DamageBoost());
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }

        if (collider2D.tag =="HealthMax")
        {
            StartCoroutine(HealthMax());
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }

        if (collider2D.tag =="IceBoost")
        {
            StartCoroutine(IceBoostCoroutine());
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }

        if (collider2D.tag == "Key")
        {
            keys += 1;
            AudioSource.PlayClipAtPoint(keySFX, Camera.main.transform.position);
        }  

        if (collider2D.tag == "Door")
        {
            if (UpgradeManager.instance.finishedLevel < myDoor.index && myDoor.LevelType == 0)
            {
                UpgradeManager.instance.finishedLevel = myDoor.index;
            }
            if (UpgradeManager.instance.finishedObstacleLevel < myDoor.index && myDoor.LevelType == 1)
            {
                UpgradeManager.instance.finishedObstacleLevel = myDoor.index;
            }
            
            myUpgradeSystem.UpdateMoney(); 
            SceneManager.LoadScene("WinScreen");
        }

        if (collider2D.tag == "Money")
        {
            money += 1;
            AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position);
        }

        if (health < 1 && playerDestroyed == false)
        { 
            StartCoroutine(DestroyPlayer());
            playerDestroyed = true;
        }

        if (collider2D.tag == "SpeedShoot")
        {
            StartCoroutine(SpeedShootCoroutine());
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }
    }

    IEnumerator SpeedShootCoroutine()
    {
        myShooting.shootspeed = myShooting.shootspeed / 3;
        mySpriteRenderer.sprite = shootSpeedSprite;
        mySpeedShootVfX = Instantiate(speedShootVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(boostTime);
        Destroy(mySpeedShootVfX);
        mySpriteRenderer.sprite = defaultPlayerSprite;
        myShooting.shootspeed = myShooting.shootspeed * 3;
    }

    IEnumerator IceBoostCoroutine()
    {
        iceBoost = true;
        mySpriteRenderer.sprite = iceBoostSprite;
        myIceBoostVFX = Instantiate(iceBoostVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(boostTime);
        mySpriteRenderer.sprite = defaultPlayerSprite;
        Destroy(myIceBoostVFX);
        iceBoost = false;
    }

    IEnumerator Hurt()
    {
        mySpriteRenderer.sprite = playerHurtSprite;
        yield return new WaitForSeconds(.175f);
        if(mySpriteRenderer != null)
        {
            mySpriteRenderer.sprite = defaultPlayerSprite;
        }    
    }

    IEnumerator DamageBoost()
    {            
        myDamageBoostEffekt = Instantiate(damageBoostVFX, transform.position, Quaternion.identity);
        mySpriteRenderer.sprite = myDamageBoostSprite;
        
        damage = damage * 2 + 1;
        yield return new WaitForSeconds(boostTime);
        damage = (damage - 1) / 2;
        Destroy(myDamageBoostEffekt);  
        mySpriteRenderer.sprite = defaultPlayerSprite;          
    }

    IEnumerator HealthMax()
    {
        myHealthMaxEffekt = Instantiate(healthMaxVFX, transform.position, Quaternion.identity);
        mySpriteRenderer.sprite = healthMaxSprite;

        takeDamage = false;
        yield return new WaitForSeconds(boostTime);
        takeDamage = true;

        Destroy(myHealthMaxEffekt); 
        mySpriteRenderer.sprite = defaultPlayerSprite;
    }

    void KeyCheck()
    {
        int maxKeys = myKeysPerLevel.GetKeysPerLevel();

        if (keys == maxKeys)
        {
            openDoor = true;
        }
    }

    IEnumerator DestroyPlayer()
    {
        Destroy(mySpriteRenderer);
        StartCoroutine(TriggerExplosionFX());
        myUpgradeSystem.UpdateMoney(); 
        yield return new WaitForSeconds(wait2);
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator TriggerExplosionFX()
    {
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVolume);
        GameObject particles = Instantiate(explodeVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(wait1);
        Destroy(particles);
    }
    public bool GetDashCoolDown()
    {
        return dashCoolDown;
    }
    public bool GetRegenerationCoolDown()
    {
        return regenerationCoolDown;
    }
    public bool GetBombCoolDown()
    {
        return bombsCoolDown;
    }

    public bool GetIceBoost()
    {
        return iceBoost;
    }
    public int GetKeys()
    {
        return keys;
    }

    public float GetHealth()
    {
        return health;
    }

    public bool OpenDoor()
    {
        return openDoor;
    }
    
    public float GetDamage()
    {
        return damage;
    }
    
    public float GetBombDamage()
    {
        return bombDamage;
    }

    public float GetMoney()
    {
        return money;
    }
}

