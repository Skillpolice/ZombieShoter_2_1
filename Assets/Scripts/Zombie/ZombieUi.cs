using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUi : MonoBehaviour
{
    public Zombie zombie;
    public Slider healthSlider;

    //public Image slide2;

    private void Start()
    {
        zombie.HealthChange += UpdateHealthBar;

        healthSlider.maxValue = zombie.healthZombie;
        healthSlider.value = zombie.healthZombie;
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = zombie.healthZombie;

    }

    private void Update()
    {
        healthSlider.value = zombie.healthZombie;
        //slide2.fillAmount = zombie.healthZombie / 100;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity; //поворот в мире 0
    }
}
