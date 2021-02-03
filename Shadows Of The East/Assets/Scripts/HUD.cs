using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image healthFilling;
    [SerializeField] private PlayerCombat playerCombat;

    public void UpdateHealth()
    {
        healthFilling.fillAmount = (float)playerCombat.health / (float)playerCombat.maxHealth;
    }
}
