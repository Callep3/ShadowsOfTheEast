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
    [SerializeField] public int lightAttackDamage = 12;
    [SerializeField] private float lightAttackCooldown = 0.5f;
    [Header("Heavy attack settings")]
    [SerializeField] public int heavyAttackDamage = 25;
    [SerializeField] private float heavyAttackCooldown = 0.8f;
    [Header("Health Settings")]
    [SerializeField] public int health = 20;
    [Header("Defence Settings")]
    [SerializeField] public int defence = 0;
    [Header("Throwables Settings")]
    [SerializeField] private GameObject shuriken;
    [SerializeField] private int throwDamage = 3;
    [SerializeField] private float throwCooldown = 0.25f;

    private bool isDead = false;
    private float attackCooldownTimer;
    private List<GameObject> Shurikens = new List<GameObject>();
    private int bonusDamage = 0;

    private void Update()
    {
        UpdateMeleeCombat();


        // Shuriken updater
        if (Shurikens.Count > 0)
        {
            for (int i = 0; i < Shurikens.Count; i++)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(Shurikens[i].transform.position, 0.5f, enemyLayers);

                foreach (Collider2D collider in hitColliders)
                {
                    IDamagable damagable = collider.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        damagable.TakeDamage(throwDamage + bonusDamage);
                        Destroy(Shurikens[i]);
                        Shurikens.Remove(Shurikens[i]);
                    }
                }
            }
        }
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

                if (Input.GetAxis("Throw") > 0)
                {
                    Throw();
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
                damagable.TakeDamage(lightAttackDamage + bonusDamage);
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
                damagable.TakeDamage(heavyAttackDamage + bonusDamage);
            }
        }

        attackCooldownTimer = heavyAttackCooldown;
    }

    private void Throw()
    {
        GameObject shurikenObject = Instantiate(shuriken, attackPoint.position, transform.rotation);
        shurikenObject.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0, 0);
        Shurikens.Add(shurikenObject);
        attackCooldownTimer = throwCooldown;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            health -= damageAmount - defence;
            if (health <= 0)
            {
                //play death anim
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
        }
    }

    public void AddBonusDamage(float duration, int damage)
    {
        bonusDamage += damage;
        StartCoroutine(RemoveBonusDamage(duration, damage));
    }

    IEnumerator RemoveBonusDamage(float duration, int damage)
    {
        yield return new WaitForSeconds(duration);

        bonusDamage -= damage;
    }
}
