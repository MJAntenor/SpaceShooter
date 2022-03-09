using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    //needed extra using of UI + TMPro

    public Image healthBarFill;
    public TextMeshProUGUI waveText;

    private void Awake()
    {
        Instance = this;
    } //weird ass one only moment, can do for stuff like EnemySpawner

    public void DisplayHealth(int currentArmor, int maxArmor)
    {
        float healthAmount = (float)currentArmor / (float)maxArmor;
        healthBarFill.fillAmount = healthAmount;
    }

    public void DisplayWave(int currentWave)
    {
        waveText.SetText("Wave: " + currentWave);
    }
}