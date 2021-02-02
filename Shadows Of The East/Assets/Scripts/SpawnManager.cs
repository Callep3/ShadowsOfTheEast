using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance = null;
    
    public GameObject spawnPositionLeft;
    public GameObject spawnPositionRight;
    public GameObject enemyPrefab;

    public int numberOfEnemies = 0;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(this);
    }
    #endregion
    
    #region Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            numberOfEnemies--;
        }
    }
    #endregion

    #region SpawnCode
    public void SpawnEnemy()
    {
        numberOfEnemies++;
        ChooseSpawnPoint();
    }

    private void ChooseSpawnPoint()
    {
        if ((int)Random.Range(0, 2) == (int)Spawn_Point.Left)
        {
            InstantiateEnemy(spawnPositionLeft);
        }
        else
        {
            InstantiateEnemy(spawnPositionRight);
        }
    }

    private void InstantiateEnemy(GameObject spawnPoint)
    {
        Instantiate(enemyPrefab, spawnPoint.transform);
    }
    #endregion
}

public enum Spawn_Point : int
{
    Left,
    Right
}