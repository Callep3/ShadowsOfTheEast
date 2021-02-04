using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLotus : MonoBehaviour
{
    [SerializeField] private float lifeTime = 15f;
    [SerializeField] private float bonusSpeed = 3f;    
    [SerializeField] private float duration = 7.5f;
    [SerializeField] private int staminaReduction = 10;
    [SerializeField] private int shurikens = 10;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private HUD hud;
    private void Start()
    {
        StartCoroutine(DespawnOverLifeTime());
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();
    }

    IEnumerator DespawnOverLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.AddMovementSpeed(duration, bonusSpeed, staminaReduction);
            Destroy(gameObject);
            playerCombat.shurikenAmount += shurikens;
            hud.UpdateShuriken();
        }
    }
}
