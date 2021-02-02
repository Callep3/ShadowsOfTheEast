using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamagable
{
    [Header("General settings")]
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [Header("Light attack settings")]
    [SerializeField] private int lightAttackDamage = 5;
    [SerializeField] private float lightAttackCooldown = 0.5f;
    [Header("Heavy attack settings")]
    [SerializeField] private int heavyAttackDamage = 10;
    [SerializeField] private float heavyAttackCooldown = 0.8f;
    [Header("Health Settings")]
    [SerializeField] private int health;

    private bool isDead = false;
    private float attackCooldownTimer;

    private void Update()
    {
        UpdateMeleeCombat();
    }

    private void UpdateMeleeCombat()
    {
        if (attackCooldownTimer <= 0)
        {
            if (Input.anyKey)
            {
                if (Input.GetAxis("LightAttack") > 0)
                {
                    LightAttack();
                }

                if (Input.GetAxis("HeavyAttack") > 0)
                {
                    HeavyAttack();
                }
            }
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }
    private void LightAttack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D collider in hitColliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(lightAttackDamage);
            }
        }

        attackCooldownTimer = lightAttackCooldown;
    }

    private void HeavyAttack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D collider in hitColliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(heavyAttackDamage);
            }
        }

        attackCooldownTimer = heavyAttackCooldown;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                //play death anim
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
        }
    }
}
