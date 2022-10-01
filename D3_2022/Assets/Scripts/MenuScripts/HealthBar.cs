using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        if(currentHealth < 0) currentHealth = 0;
        GetComponentInChildren<Text>().text = currentHealth.ToString("00") + " / " + maxHealth.ToString("00");
    }
}
