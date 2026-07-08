using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float health = 3f;
    [SerializeField] float seeRange = 30f;
    [SerializeField] float rangedSeeRange = 30f;
    [SerializeField] float damage = 1;
    [SerializeField] float rangedDamage = 1;

    [Header("VFX, SFX")]
    [SerializeField] GameObject explodeVFX;
    [SerializeField] AudioClip explodeSFX;
    [SerializeField] float explosionSFXVolume;
    
    [Header("Sprites")]
    [SerializeField] Sprite defaultEnemySprite;
    [SerializeField] Sprite enemyHurtSprite;
    [SerializeField] Sprite slowEnemySprite;

    [Header("Scene")]
    [SerializeField] Transform playerNormal;
    [SerializeField] GameObject money;

    [Header("Time")]
    [SerializeField] float wait;

    bool canShoot = false;
    bool iceBoost = false;
    bool destroyEnemyIsRunning = false;

    Player myPlayer;
    Rigidbody2D myRigidbody2D;
    Transform myTransform;
    AIPath myAIPath;
    SpriteRenderer mySpriteRenderer;
    EnemyShooting myEnemyShooting;

    void Start()
    {
        myPlayer = FindAnyObjectByType<Player>();
        myAIPath = GetComponent<AIPath>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myEnemyShooting = GetComponent<EnemyShooting>();
    }

    void Update()
    {
        iceBoost = myPlayer.GetIceBoost();

        Rotation();
        Death();
        Sleep();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (iceBoost == true && collider2D.tag == "Bullet")
        {
            StartCoroutine(Slow());
            float hurt = myPlayer.GetDamage();
            health -= hurt;
        }

        else if (collider2D.tag == "Bullet")
        {
            float hurt = myPlayer.GetDamage();
            health -= hurt;
            StartCoroutine(Hurt());
        }

        if (collider2D.tag == "Player")
        {
            health = 0f;
        }
        
        if(collider2D.tag =="Explosion")
        {
            float bombHurt = myPlayer.GetBombDamage();
            health -= bombHurt;
            StartCoroutine(Hurt());
        }
    }
    IEnumerator Slow()
    {
        mySpriteRenderer.sprite = slowEnemySprite;
        PlayerPrefs.SetFloat("LastMaxSpeed", myAIPath.maxSpeed);
        PlayerPrefs.Save();
        myAIPath.maxSpeed = 5f;

        if (myEnemyShooting != null)
        {
            PlayerPrefs.SetFloat("LastShootTime", myEnemyShooting.shootWaitTime);
            PlayerPrefs.Save();
            myEnemyShooting.shootWaitTime = 3f;
        }
        yield return new WaitForSeconds(5f);

        if (myEnemyShooting != null)
        {
            myEnemyShooting.shootWaitTime = PlayerPrefs.GetFloat("LastShootTime");
        }
        myAIPath.maxSpeed = PlayerPrefs.GetFloat("LastMaxSpeed");
        mySpriteRenderer.sprite = defaultEnemySprite;
    }

    IEnumerator Hurt()
    {
        mySpriteRenderer.sprite = enemyHurtSprite;
        yield return new WaitForSeconds(.175f);
        mySpriteRenderer.sprite = defaultEnemySprite;
    }

    private void Death()
    {
        if (health <= 0f && destroyEnemyIsRunning == false)
        {
            StartCoroutine(DestroyEnemy());
        }
    }

    IEnumerator DestroyEnemy()
    {
        destroyEnemyIsRunning = true;
        Destroy(mySpriteRenderer);
        StartCoroutine(TriggerExplosionFX());
        Instantiate(money, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }

    IEnumerator TriggerExplosionFX()
    {
        AudioSource.PlayClipAtPoint(explodeSFX, Camera.main.transform.position, explosionSFXVolume);
        GameObject particles = Instantiate(explodeVFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(wait);
        Destroy(particles);
    }

    private void Rotation()
    {
        Vector3 direction = playerNormal.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myRigidbody2D.rotation = angle - 180f;
    }

    private void Sleep()
    {
        myAIPath.canMove = false;

        if ((myTransform.position - playerNormal.position).magnitude < seeRange)
        {
            myAIPath.canMove = true;
        }

        if ((myTransform.position - playerNormal.position).magnitude < rangedSeeRange)
        {
            canShoot = true; 
        }

        if ((myTransform.position - playerNormal.position).magnitude > rangedSeeRange)
        {
            canShoot = false; 
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetRangedDamage()
    {
        return rangedDamage;
    }

    public bool GetShootingAllowed()
    {
        return canShoot;
    }
}