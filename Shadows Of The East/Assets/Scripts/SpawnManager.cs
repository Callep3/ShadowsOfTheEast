using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnPositionLeft;
    public GameObject spawnPositionRight;
    public GameObject enemyPrefab;
    private bool LeftOrRightSpawnPoint;

    #region Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }
    #endregion

    #region SpawnCode
    public void SpawnEnemy()
    {
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