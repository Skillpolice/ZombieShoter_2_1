using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    Player player;
    public int addMoney;

    private void ApplyEffect()
    {
        player = FindObjectOfType<Player>();
        player.scoreMoney += addMoney;
        player.playerMoneyText.text = "Moneys: " + player.scoreMoney.ToString();
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
