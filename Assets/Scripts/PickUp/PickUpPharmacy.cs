using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPharmacy : MonoBehaviour
{
    Player player;
    MainUI mainUI;
    public int healthUp;

    public void ApplyEffect()
    {
        mainUI = FindObjectOfType<MainUI>();
        player = FindObjectOfType<Player>();
        
        player.healthPlayer += healthUp;
        mainUI.playerHealth.value += healthUp;
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
