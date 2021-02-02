using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;

    [SerializeField] private GameObject redLotusPrefab;
    [SerializeField] private GameObject blueLotusPrefab;

    [SerializeField] private int minimumBeforeDrop = 3;
    [SerializeField] private int maximumBeforeDrop = 15;
    [SerializeField] private int maximumDifferenceBetweenDrops = 3;

    private int killsWithoutDrop;

    private int blueLotusDrops;
    private int redLotusDrops;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public GameObject GetDrop()
    {
        if (killsWithoutDrop >= maximumBeforeDrop)
        {
            killsWithoutDrop = 0;
            return GetRandomLotus();
        }
        else if (killsWithoutDrop >= minimumBeforeDrop)
        {
            int random = Random.Range(0, 100);
            if (random < 69)
            {
                killsWithoutDrop = 0;
                return GetRandomLotus();
            }
            else
            {
                killsWithoutDrop++;
                return null;
            }
        }
        else
        {
            killsWithoutDrop++;
            return null;
        }
    }

    private GameObject GetRandomLotus()
    {
        if (blueLotusDrops - redLotusDrops >= maximumDifferenceBetweenDrops)
        {
            blueLotusDrops++;
            return blueLotusPrefab;
        }
        else if (redLotusDrops - blueLotusDrops >= maximumDifferenceBetweenDrops)
        {
            redLotusDrops++;
            return redLotusPrefab;
        }
        else
        {
            if (Random.Range(0,2) == 0)
            {
                redLotusDrops++;
                return redLotusPrefab;
            }
            else
            {
                blueLotusDrops++;
                return blueLotusPrefab;
            }
        }
    }
}
