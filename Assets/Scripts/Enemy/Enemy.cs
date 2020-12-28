using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Player player;
    Animator animator;
    CircleCollider2D coll2D;

    public Text enemyHealthText;
    public GameObject shootPos;

    [Header("Bullet")]
    public Bullet bulletPrefab;
    public float fireRotate;

    [Header("Enemy")]
    public int bullDamage;
    public int healthEnemy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        coll2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemyHealthText.text = "Enemy: " + healthEnemy.ToString();

        StartCoroutine(EnemyFire(fireRotate));
    }
    IEnumerator EnemyFire(float fire)
    {
        for (int i = 0; i < healthEnemy; i++)
        {
            yield return new WaitForSeconds(fire);
            Instantiate(bulletPrefab, shootPos.transform.position, transform.rotation);
            animator.SetTrigger("Attack");
        }
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthEnemy -= player.bullDamagePlayer;
        enemyHealthText.text = "Enemy: " + healthEnemy.ToString();
        if (healthEnemy <= 50)
        {
            enemyHealthText.color = Color.red;
        }
        if (healthEnemy <= 0)
        {
            enemyHealthText.text = "Enemy: Dead";
            animator.SetTrigger("Death");
            coll2D.enabled = false;
            StopAllCoroutines();
            return;
        }

    }
}
