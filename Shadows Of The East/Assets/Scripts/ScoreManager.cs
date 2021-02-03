using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;

    public int baseScorePerKill = 10;
    public int score;
    public int combo;
    public int comboTimer = 5;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void IncreaseCombo()
    {
        combo++;
        StartCoroutine(ComboTimer(combo));
        GameManager.Instance.UpdateComboText();
    }

    private IEnumerator ComboTimer(int currentCombo)
    {
        yield return new WaitForSeconds(comboTimer);
        if (combo == currentCombo)
        {
            combo = 0;
            GameManager.Instance.UpdateComboText();
        }
    }
    
    public void IncreaseScore()
    {
        score += baseScorePerKill * combo;
        
        GameManager.Instance.UpdateScoreText();
    }
}
