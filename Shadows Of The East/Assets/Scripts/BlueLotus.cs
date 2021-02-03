using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLotus : MonoBehaviour
{
    [SerializeField] private float lifeTime = 15f;
    [SerializeField] private float bonusSpeed = 3f;    
    [SerializeField] private float duration = 7.5f;
    [SerializeField] private int staminaReduction = 10;
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
        Debug.Log(other.gameObject.name);
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.AddMovementSpeed(duration, bonusSpeed, staminaReduction);
            Destroy(gameObject);
        }
    }
}
