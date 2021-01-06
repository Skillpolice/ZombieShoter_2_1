using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    GameManager  gameManager;
    public int addMoney;

    private void ApplyEffect()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.AddMoney(addMoney);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyEffect();
            Destroy(gameObject);
        }
    }
}
