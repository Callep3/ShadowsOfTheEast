using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    public static GhostSpawnManager Instance = null;

    [Header("Setup settings")]
    [SerializeField] private GameObject spawnPositionLeft;
    [SerializeField] private GameObject spawnPositionRight;
    [SerializeField] private GameObject ghostPrefab;
    [Header("Spawn settings")]
    [SerializeField] private int maxAmountOfGhosts = 4;
    [SerializeField] private float minimumSpawnTime = 5f;
    [SerializeField] private float maximumSpawnTime = 25f;
    public int numberOfActiveGhosts = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnGhosts()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minimumSpawnTime, maximumSpawnTime));

            if (numberOfActiveGhosts < maxAmountOfGhosts)
            {
                Debug.Log("Spawned ghost");
                SpawnGhost();
            }
        }
    }

    public void SpawnGhost()
    {
        numberOfActiveGhosts++;
        ChooseSpawnPoint();
    }

    private void ChooseSpawnPoint()
    {
        if ((int)Random.Range(0, 2) == (int)Spawn_Point.Left)
        {
            InstantiateGhost(spawnPositionLeft);
        }
        else
        {
            InstantiateGhost(spawnPositionRight);
        }
    }

    private void InstantiateGhost(GameObject spawnPoint)
    {
        Instantiate(ghostPrefab, spawnPoint.transform);
    }
}
