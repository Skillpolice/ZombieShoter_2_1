using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Zombie zombie;
    Enemy enemy;
    Animator animator;
    CircleCollider2D coll2D;
    GameManager gameManager;

    [Header("Text")]
    public Text playerHealthText;

    [Header("Bullet Obj")]
    public Bullet bulletPrefab;
    public GameObject shootPosBullet;

    [Header("Bullet")]
    public float fireRotate; //частота стрельбы
    public int bullDamagePlayer;

    [Header("Player")]
    public int healthPlayer;

    float nextFire; //сколько прошло времени от предыдущего выстрела

    private void Awake()
    {
        animator = GetComponent<Animator>();
        coll2D = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemy = FindObjectOfType<Enemy>();
        zombie = FindObjectOfType<Zombie>();

        playerHealthText.text = "Player: " + healthPlayer.ToString();
    }

    private void Update()
    {
        CheckFire();
    }

    private void CheckFire()
    {
        if (healthPlayer > 0)
        {
            if (Input.GetButtonDown("Fire1") && nextFire <= 0)
            {
                Attack();
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

    private void Attack()
    {
        Instantiate(bulletPrefab, shootPosBullet.transform.position, transform.rotation); //Создание пули , префаб, откуда идем выстрел и нужное вращение
        nextFire = fireRotate;
        animator.SetTrigger("Attack");
    }


    public void UpdateHealth(int amount)
    {
        healthPlayer -= amount;

        playerHealthText.text = "Player: " + healthPlayer.ToString();

        if (healthPlayer <= 50)
        {
            playerHealthText.color = Color.red;
        }
        if (healthPlayer <= 0)
        {
            playerHealthText.text = "Player: Dead";
            animator.SetTrigger("Death");
            gameManager.RestartGame();
            coll2D.enabled = false;
        }

    }

}
