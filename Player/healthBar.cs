using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class healthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Text HealthText;

    public void setHealthUI(float health, float maxHealth)
    {
        HealthSlider.value = health / maxHealth;
        HealthText.text = Mathf.Ceil(health).ToString() + "/" + maxHealth.ToString();
    }     
}
