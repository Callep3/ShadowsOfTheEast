using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance = null;
    
    public GameObject spawnPositionLeft;
    public GameObject spawnPositionRight;
    public GameObject spawnBossPositionLeft;
    public GameObject spawnBossPositionRight;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    public int numberOfEnemies = 0;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
    #endregion

    #region SpawnCode
    public void SpawnEnemy()
    {
        numberOfEnemies++;
        ChooseSpawnPoint();
        GameManager.Instance.UpdateEnemiesLeftText();
    }

    private void ChooseSpawnPoint()
    {
        if ((int)Random.Range(0, 2) == (int)Spawn_Point.Left)
        {
            InstantiateEnemy(enemyPrefab, spawnPositionLeft);
        }
        else
        {
            InstantiateEnemy(enemyPrefab, spawnPositionRight);
        }
    }

    private void InstantiateEnemy(GameObject enemy, GameObject spawnPoint)
    {
        Instantiate(enemy, spawnPoint.transform);
    }

    public void SpawnBoss()
    {
        numberOfEnemies++;
        if ((int)Random.Range(0, 2) == (int)Spawn_Point.Left)
        {
            InstantiateEnemy(bossPrefab, spawnBossPositionLeft);
        }
        else
        {
            InstantiateEnemy(bossPrefab, spawnBossPositionRight);
        }
        GameManager.Instance.UpdateEnemiesLeftText();
    }
    #endregion
}

public enum Spawn_Point : int
{
    Left,
    Right
}