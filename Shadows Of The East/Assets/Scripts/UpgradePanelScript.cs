using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradePanelScript : MonoBehaviour
{
    [SerializeField] private HUD hud;
    private PlayerCombat playerCombat;

    public int lightAttackIncrease = 5;
    public int heavyAttackIncrease = 10;
    public int throwDamageIncrease = 3;
    
    public int defenceIncrease = 10;

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
        playerCombat.throwDamage += throwDamageIncrease;

        UpgradeButtons();
    }
    
    public void DefenceUpButton()
    {
        playerCombat.defence += 10;
        
        UpgradeButtons();
    }
    
    public void HealthUpButton()
    {
        playerCombat.maxHealth += healthIncrease;
        playerCombat.health = playerCombat.maxHealth;
        hud.UpdateHealth();
        
        UpgradeButtons();
    }

    private void UpgradeButtons()
    {
        GameManager.Instance.bought = true;
        GameManager.Instance.UpgradePanel.SetActive(false);
    }
    #endregion
}
