using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradePanelScript : MonoBehaviour
{
    private PlayerCombat playerCombat;

    public int lightAttackIncrease = 5;
    public int heavyAttackIncrease = 10;

    public int healthIncrease = 20;
    #region ButtonFunctionality

    private void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    public void AttackUpButton()
    {
        playerCombat.lightAttackDamage += lightAttackIncrease;
        playerCombat.heavyAttackDamage += heavyAttackIncrease;

        UpgradeButtons();
    }
    
    public void DefenceUpButton()
    {
        playerCombat.defence += 5;
        
        UpgradeButtons();
    }
    
    public void HealthUpButton()
    {
        playerCombat.health += healthIncrease;
        
        UpgradeButtons();
    }

    private void UpgradeButtons()
    {
        GameManager.Instance.bought = true;
        GameManager.Instance.UpgradePanel.SetActive(false);
    }
    #endregion
}
