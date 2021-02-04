using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnvironmentSpawnManager : MonoBehaviour
{
    public static FlyingEnvironmentSpawnManager Instance = null;

    [Header("Setup settings")]
    [SerializeField] private GameObject spawnPositionLeft;
    [SerializeField] private GameObject spawnPositionRight;
    [Header("Ghost Spawn settings")]
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private int maxAmountOfGhosts = 4;
    [SerializeField] private float minimumGhostSpawnTime = 5f;
    [SerializeField] private float maximumGhostSpawnTime = 25f;
    public int numberOfActiveGhosts = 0;
    [Header("Bird Spawn Settings")]
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private int maxAmountOfBirds = 4;
    [SerializeField] private float minimumBirdsSpawnTime = 5f;
    [SerializeField] private float maximumBirdsSpawnTime = 25f;
    public int numberOfActiveBirds = 0;

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
        StartCoroutine(SpawnBirds());
    }

    IEnumerator SpawnGhosts()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minimumGhostSpawnTime, maximumGhostSpawnTime));

            if (numberOfActiveGhosts < maxAmountOfGhosts)
            {
                SpawnGhost();
            }
        }
    }

    IEnumerator SpawnBirds()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minimumBirdsSpawnTime, maximumBirdsSpawnTime));

            if (numberOfActiveBirds < maxAmountOfBirds)
            {
                SpawnBird();
            }
        }
    }    
    public void SpawnGhost()
    {
        numberOfActiveGhosts++;
        ChooseSpawnPoint(ghostPrefab);
    }

    public void SpawnBird()
    {
        numberOfActiveBirds++;
        ChooseSpawnPoint(birdPrefab);
    }

    private void ChooseSpawnPoint(GameObject prefab)
    {
        if ((int)Random.Range(0, 2) == (int)Spawn_Point.Left)
        {
            Instantiate(prefab, spawnPositionLeft);
        }
        else
        {
            Instantiate(prefab, spawnPositionRight);
        }
    }

    private void Instantiate(GameObject prefab,GameObject spawnPoint)
    {
        Instantiate(prefab, spawnPoint.transform);
    }
}
