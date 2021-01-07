using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    Player player;
    public int addAmmo;

    private void ApplyEffect()
    {
        player = FindObjectOfType<Player>();
        player.maxClips += addAmmo;
        player.playerAmmo.text = "Ammo: " + player.currenAmmo + " / " + player.maxClips.ToString();
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
