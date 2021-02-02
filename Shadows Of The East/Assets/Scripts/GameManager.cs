using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int wave = 0;
    public TMP_Text wave_Text;

    public float enemySpawnCooldown;
    public float timeInBetweenWaves;

    private int numberOfEnemiesToSpawn;
    public int baseEnemiesPerWave = 10;
    public int enemyNumberIncreasePerWaveConstant;

    private void Start()
    {
        NextWave();

        if (wave_Text == null)
        {
            if (GameObject.Find("IngameUI_Panel/Wave_Text"))
            {
                wave_Text = GameObject.Find("IngameUI_Panel/Wave_Text").GetComponent<TMP_Text>();
            }
            else
            {
                Debug.LogError("Wave_Text missing in IngameUI_Panel");
            }
        }
    }

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
    }
    #endregion

    #region WaveFunctionality
    private void NextWave()
    {
        wave++;

        CalculateNumberOfEnemiesToSpawn();
        
        StartCoroutine(EnemySpawner());
        
        UpdateWaveText();
    }

    private void CalculateNumberOfEnemiesToSpawn()
    {
        numberOfEnemiesToSpawn = (int) (baseEnemiesPerWave * (enemyNumberIncreasePerWaveConstant * wave));
    }

    private IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(timeInBetweenWaves);
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(enemySpawnCooldown);
            SpawnManager.Instance.SpawnEnemy();
        }
    }

    private void UpdateWaveText()
    {
        wave_Text.text = "Wave: " + wave;
    }
    #endregion
}
