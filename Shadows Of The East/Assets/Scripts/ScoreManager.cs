using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;

    public int baseScorePerKill = 10;
    public int score;
    public int combo;
    public int comboTimer = 5;
    private Camera camera;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        camera = Camera.main;
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

    public void ShakeCamera()
    {
        StartCoroutine(ResetCamera());
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(0);
        camera.DOShakePosition(1, 1);
        yield return new WaitForSeconds(1);
        camera.transform.DOMove(new Vector3(0, 0, -10), 1);
    }
}