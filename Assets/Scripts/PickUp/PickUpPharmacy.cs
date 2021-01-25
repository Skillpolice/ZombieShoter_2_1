using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPharmacy : MonoBehaviour
{
    Player player;
    MainUI mainUI;

    public int healthUp;

    private void Start()
    {
        mainUI = MainUI.Instance;
        player = Player.Instance;
    }

    public void ApplyEffect()
    {
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
