using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    Player player;

    public static MainUI Instance;

    [Header("Game Obj")]
    public GameObject gameOverPanel;

    [Header("Sliders")]
    public Slider playerHealth;
    public Slider playerAmmo;
    //TODO public Image playerPortrait;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = Player.Instance;
        player.OnHealthChange += UpdateHealth; //������� ������� ��������� ��������
        player.OnDeath += ShowGameOver;

        playerHealth.maxValue = player.HealthPlayer;
        playerHealth.value = player.HealthPlayer;

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
        playerHealth.value = player.HealthPlayer;
    }

}
