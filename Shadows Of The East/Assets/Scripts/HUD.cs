using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image healthFilling;
    [SerializeField] private Image manaFilling;
    [SerializeField] private TMP_Text shurikenCounter;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerMovement playerMovement;

    public void UpdateHealth()
    {
        healthFilling.fillAmount = (float)playerCombat.health / (float)playerCombat.maxHealth;
    }

    public void UpdateMana()
    {
        manaFilling.fillAmount = (float) playerMovement.stamina / (float) playerMovement.maxStamina;
    }

    public void UpdateShuriken()
    {
        shurikenCounter.text = "" + playerCombat.shurikenAmount;
    }
}
