using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    Player player;

    [Header("Game Obj")]
    public GameObject gameOverPanel;

    [Header("Sliders")]
    public Slider playerHealth;
    public Slider playerAmmo;
    //TODO public Image playerPortrait;

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.OnHealthChange += UpdateHealth; //делегат события изменения здоровья
        player.OnDeath += ShowGameOver;

        playerHealth.maxValue = player.healthPlayer;
        playerHealth.value = player.healthPlayer;

        playerAmmo.maxValue = player.maxAmmo;
        playerAmmo.value = player.currenAmmo;
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void Update()
    {
        playerAmmo.value = player.currenAmmo;
    }

    private void UpdateHealth()
    {
        playerHealth.value = player.healthPlayer;
    }
}
