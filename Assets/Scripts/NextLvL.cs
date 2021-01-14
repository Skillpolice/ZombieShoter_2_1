using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvL : MonoBehaviour
{
    Player player;

    public int healthBoss;
    int index;
    private void ApplyEffect()
    {
        player = FindObjectOfType<Player>();

        healthBoss -= player.bullDamagePlayer;
        if (healthBoss <= 0)
        {
            LoadScene();

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyEffect();
        }
    }

    private void LoadScene()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}
