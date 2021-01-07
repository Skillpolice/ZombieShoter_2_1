using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Player player;
    public Slider healthSlider;
    public Slider ammoSlider;

    private void Start()
    {
        healthSlider.maxValue = player.healthPlayer;
        healthSlider.value = player.healthPlayer;

        ammoSlider.maxValue = player.maxAmmo;
        ammoSlider.value = player.currenAmmo;
    }

    private void Update()
    {
        healthSlider.value = player.healthPlayer;

        ammoSlider.value = player.currenAmmo;
    }
}
