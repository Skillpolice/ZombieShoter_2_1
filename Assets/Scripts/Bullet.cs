using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;

    public float bullSpeed;
    int bullDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = Player.Instance;
    }

    private void OnEnable()
    {
        rb.velocity = -transform.up * bullSpeed; //Скорость пули - стреляет куда смотрит
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.UpdateHealth(bullDamage);
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Walls"))
        {
            LeanPool.Despawn(gameObject);
        }
    }

    private void OnBecameInvisible() //Уничтожение обьектов за пределы камеры
    {
        if (gameObject.activeSelf)
        {
            LeanPool.Despawn(gameObject);
        }

    }
}
