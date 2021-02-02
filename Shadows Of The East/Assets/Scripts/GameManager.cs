using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    private int wave = 0;
    public TMP_Text wave_Text;
    public TMP_Text enemiesLeft_Text;
    public GameObject UpgradePanel;

    public float enemySpawnCooldown;
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

    #region Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            NextWave();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(SpawnManager.Instance.numberOfEnemies);
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpgradePanel.SetActive(true);
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
            yield return new WaitForSeconds(enemySpawnCooldown);
            SpawnManager.Instance.SpawnEnemy();
        }

        doneSpawning = true;
    }

    public IEnumerator ForceNextWave(int currentWave)
    {
        yield return new WaitForSeconds(baseTimePerWave + timePerWave * wave);
        if (wave != currentWave)
        {
            Debug.Log("not current wave");
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
    #endregion
}
