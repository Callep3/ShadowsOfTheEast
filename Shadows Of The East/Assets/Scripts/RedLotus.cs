using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLotus : MonoBehaviour
{
    [SerializeField] private float lifeTime = 15f;
    [SerializeField] private int bonusDamage = 5;
    [SerializeField] private float duration = 7.5f;
    private void Start()
    {
        StartCoroutine(DespawnOverLifeTime());
    }

    IEnumerator DespawnOverLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCombat combat = other.GetComponent<PlayerCombat>();
        if (combat != null)
        {
            combat.AddBonusDamage(duration, bonusDamage);
            Destroy(gameObject);
        }
    }
}
