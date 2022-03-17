using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class energyBar : MonoBehaviour
{
    public Slider energySlider;
    public Text energyText;
    
    public void setEnergyUI(float energy, float maxEnergy)
    {
        energySlider.value = energy / maxEnergy;
        energyText.text = Mathf.Ceil(energy).ToString() + "/" + maxEnergy.ToString();
    }

}
