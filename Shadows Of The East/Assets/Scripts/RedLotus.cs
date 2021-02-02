using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLotus : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private void Start()
    {
        StartCoroutine(DespawnOverLifeTime());
    }

    IEnumerator DespawnOverLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
