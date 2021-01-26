﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class Player : MonoBehaviour
{
    //Zombie zombie;
    //Enemy enemy;
    Animator animator;
    AudioSource audioSource;
    CircleCollider2D coll2D;
    GameManager gameManager;

    private static Player instance;
    public static Player Instance
    {
        get { return instance; }
    }

    public Action OnHealthChange = delegate { }; //пустой делегат ,что бы не выскакивало ошибок
    public Action OnDeath = delegate { };

    [Header("Text")]
    //public Text playerHealthText;
    public Text playerAmmo;
    public Text reloadAmmo;

    [Header("Bullet Obj")]
    public Bullet bulletPrefab;
    public GameObject shootPosGun;


    [Header("Gun")]
    public float fireRotate; //частота стрельбы
    public int bullDamagePlayer;
    public int maxAmmo;
    public int maxClips;
    public float reloadTime;
    [HideInInspector]
    public int currenAmmo = 0;
    private bool isreloding = false;

    [Header("Player")]
    [SerializeField] private int healthPlayer;
    public AnimatorOverrideController[] animatorOverrideController;

    public int HealthPlayer
    {
        get { return healthPlayer; }
        set { healthPlayer = value; }
    }

    float nextFire; //сколько прошло времени от предыдущего выстрела\


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        coll2D = GetComponent<CircleCollider2D>();
        gameManager = GetComponent<GameManager>();

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    private void Start()
    {
        //enemy = FindObjectOfType<Enemy>();
        //zombie = FindObjectOfType<Zombie>();

        currenAmmo = maxAmmo;

        //playerHealthText.text = "Player: " + healthPlayer.ToString();
        playerAmmo.text = currenAmmo + " / " + maxClips.ToString();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, (shootPosBullet.transform.position - transform.position) * 10, Color.green);
        if (Input.GetKey(KeyCode.Alpha1))
        {
            animator.runtimeAnimatorController = animatorOverrideController[0];
        }
        playerAmmo.text = currenAmmo + " / " + maxClips.ToString();
        CheckFire();
    }

    public void CheckFire()
    {
        if (healthPlayer > 0)
        {
            if (isreloding)
            {
                return;
            }
            if (currenAmmo <= 0)
            {
                reloadAmmo.enabled = true;
                if (Input.GetKeyDown(KeyCode.R) && maxClips > 0)
                {
                    StartCoroutine(ReloadFire());
                }
                return;
            }
            if (Input.GetKeyDown(KeyCode.R) && maxClips > 0)
            {
                if (currenAmmo < maxAmmo)
                {
                    StartCoroutine(ReloadFire());
                }
            }
            if (maxClips < 0)
            {
                StopCoroutine(ReloadFire());
                return;
            }

            if (Input.GetButtonDown("Fire1") && nextFire <= 0)
            {
                Shoot();
            }
            if (nextFire > 0)
            {
                nextFire -= Time.deltaTime;
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator ReloadFire()
    {
        isreloding = true;
        print("Reloading..");

        maxClips--;

        yield return new WaitForSeconds(reloadTime);

        playerAmmo.text = currenAmmo + " / " + maxClips.ToString();

        currenAmmo = maxAmmo;
        reloadAmmo.enabled = false;
        isreloding = false;
    }

    private void Shoot()
    {
        currenAmmo--;
        playerAmmo.text = currenAmmo + " / " + maxClips.ToString();

        LeanPool.Spawn(bulletPrefab, shootPosGun.transform.position, transform.rotation); //Создание пули , префаб, откуда идет выстрел и нужное вращение
        nextFire = fireRotate;

        audioSource.Play();
        animator.SetTrigger("Attack");

    }

    public void UpdateHealth(int amount)
    {
        healthPlayer -= amount;
        OnHealthChange();

        if (healthPlayer <= 0)
        {
            animator.SetTrigger("Death");
            //gameManager.RestartGame();
            coll2D.enabled = false;
            OnDeath();
        }

    }

    private void OnDestroy()
    {

    }

}
