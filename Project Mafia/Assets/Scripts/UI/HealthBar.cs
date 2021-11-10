using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public Gradient gradient;

    public Image fill;

    private void OnEnable()
    {
        PlayerController.PlayerHealth += SetHealth;
    }

    private void OnDisable()
    {
        PlayerController.PlayerHealth -= SetHealth;
    }

    private void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
}
