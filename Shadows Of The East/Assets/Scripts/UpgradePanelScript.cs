using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradePanelScript : MonoBehaviour
{
    #region ButtonFunctionality
    public void AttackUpButton()
    {
        //Add code for increase of player attack
        Debug.Log("AttackUpButton pressed");

        UpgradeButtons();
    }
    
    public void DefenceUpButton()
    {
        //Add code for increase of player defence
        Debug.Log("DefenceUpButton pressed");
        
        UpgradeButtons();
    }
    
    public void HealthUpButton()
    {
        //Add code for increase of player health
        Debug.Log("HealthUpButton pressed");
        
        UpgradeButtons();
    }

    private void UpgradeButtons()
    {
        GameManager.Instance.bought = true;
        GameManager.Instance.UpgradePanel.SetActive(false);
    }
    #endregion
}
