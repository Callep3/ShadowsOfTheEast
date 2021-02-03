using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    public int wave = 0;
    public TMP_Text enemiesLeft_Text;
    public TMP_Text combo_Text;
    public TMP_Text score_Text;
    public TMP_Text wave_Text;
    public GameObject UpgradePanel;

    public float enemyMinSpawnCooldown;
    public float enemyMaxSpawnCooldown;
    public float timeInBetweenWaves;

    private int numberOfEnemiesToSpawn;
    public int baseEnemiesPerWave = 10;
    public int enemyNumberIncreasePerWaveConstant;
    public int baseTimePerWave = 30;
    public int timePerWave = 10;

    public bool bought = true;
    public bool doneSpawning = true;

    #region StartFunctions
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        GetComponents();
    }

    private void Start()
    {
        NextWave();
        doneSpawning = false;
    }

    private void GetComponents()
    {
        if (wave_Text == null)
        {
            if (GameObject.Find("IngameUI_Panel/Wave_Text"))
                wave_Text = GameObject.Find("IngameUI_Panel/Wave_Text").GetComponent<TMP_Text>();
            else
                Debug.LogError("Wave_Text missing in IngameUI_Panel");
        }
        
        if (enemiesLeft_Text == null)
        {
            if (GameObject.Find("IngameUI_Panel/EnemiesLeft_Text"))
                enemiesLeft_Text = GameObject.Find("IngameUI_Panel/EnemiesLeft_Text").GetComponent<TMP_Text>();
            else
                Debug.LogError("EnemiesLeft_Text missing in IngameUI_Panel");
        }
    }
    #endregion

    #region WaveFunctionality
    public void NextWave()
    {
        if (doneSpawning == false)
        {
            return;
        }

        doneSpawning = false;
        
        wave++;

        CalculateNumberOfEnemiesToSpawn();
        
        StartCoroutine(EnemySpawner());

        StartCoroutine(ForceNextWave(wave));
        
        UpdateWaveText();
    }

    private void CalculateNumberOfEnemiesToSpawn()
    {
        numberOfEnemiesToSpawn = (int) (baseEnemiesPerWave * (enemyNumberIncreasePerWaveConstant * wave));
    }

    private IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(timeInBetweenWaves);
        if (!bought)
            UpgradePanel.SetActive(true);
        

        yield return new WaitUntil(() => bought);
        bought = false;
        
        yield return new WaitForSeconds(timeInBetweenWaves);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(Random.Range(enemyMinSpawnCooldown, enemyMaxSpawnCooldown));
            SpawnManager.Instance.SpawnEnemy();
        }

        doneSpawning = true;
    }

    public IEnumerator ForceNextWave(int currentWave)
    {
        yield return new WaitForSeconds(baseTimePerWave + timePerWave * wave);
        if (wave != currentWave)
        {
            doneSpawning = false;
        }
        else
            doneSpawning = true;
        
        NextWave();
    }
    #endregion

    #region UpdateText
    public void UpdateEnemiesLeftText()
    {
        if (enemiesLeft_Text != null)
            enemiesLeft_Text.text = "Enemies Left: " + SpawnManager.Instance.numberOfEnemies;
        else
            Debug.LogError("enemiesLeft_Text cannot be found");
    }
    
    private void UpdateWaveText()
    {
        wave_Text.text = "Wave: " + wave;
    }

    public void UpdateComboText()
    {
        combo_Text.text = "Combo: " + ScoreManager.Instance.combo;
    }

    public void UpdateScoreText()
    {
        score_Text.text = "Score: " + ScoreManager.Instance.score;
    }
    #endregion
}
