using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPharmacy : MonoBehaviour
{
    Player player;
    public int healthUp;

    private void ApplyEffect()
    {
        player = FindObjectOfType<Player>();
        player.healthPlayer += healthUp;
        player.playerHealthText.text = "Player: " + player.healthPlayer.ToString();
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
