using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamagable
{
    [Header("General settings")]
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private HUD hud;
    [Header("Light attack settings")]
    [SerializeField] public int lightAttackDamage = 12;
    [SerializeField] private float lightAttackCooldown = 0.5f;
    [Header("Heavy attack settings")]
    [SerializeField] public int heavyAttackDamage = 25;
    [SerializeField] private float heavyAttackCooldown = 0.8f;
    [Header("Health Settings")]
    [SerializeField] public int maxHealth = 20;
    [SerializeField] public int health = 20;
    [Header("Defence Settings")]
    [SerializeField] public int defence = 0;
    [Header("Throwables Settings")]
    [SerializeField] private GameObject shuriken;
    [SerializeField] public int throwDamage = 3;
    [SerializeField] private float throwCooldown = 0.25f;
    [SerializeField] private float throwSpeed = 12;
    [SerializeField] private int shurikenAmount = 100;
    [Header("Fireballs")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private int fireDamage = 80;
    [SerializeField] private float fireCooldown = 5;
    [SerializeField] private float fireCooldownTimer = 0;
    [SerializeField] private float fireSpeed = 10;

    private bool isDead = false;
    private float attackCooldownTimer;
    private List<GameObject> shurikens = new List<GameObject>();
    private List<GameObject> firballs = new List<GameObject>();
    private int bonusDamage = 0;

    private void Update()
    {
        UpdateMeleeCombat();
        UpdateShurikens();
        UpdateFireballs();
    }



    private void UpdateShurikens()
    {
        if (shurikens.Count > 0)
        {
            List<GameObject> toRemoveShurikens = new List<GameObject>();
            for (int i = 0; i < shurikens.Count; i++)
            {
                if (shurikens[i] != null)
                {
                    if (shurikens[i].transform.position.x < 35 && shurikens[i].transform.position.x > -35 && shurikens[i].transform.position.y > -3.39f)
                    {
                        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(shurikens[i].transform.position, 0.15f, enemyLayers);
                        bool enemyHit = false;
                        foreach (Collider2D collider in hitColliders)
                        {
                            IDamagable damagable = collider.GetComponent<IDamagable>();
                            if (damagable != null)
                            {
                                damagable.TakeDamage(throwDamage);
                                enemyHit = true;
                            }
                        }

                        if (enemyHit)
                        {
                            toRemoveShurikens.Add(shurikens[i]);
                            shurikens[i].SetActive(false);
                        }
                    }
                    else
                    {
                        shurikens[i].SetActive(false);
                        toRemoveShurikens.Add(shurikens[i]);
                    }
                }
            }

            foreach (GameObject shuriken in toRemoveShurikens)
            {
                shurikens.Remove(shuriken);
                Destroy(shuriken);
            }
        }
    }

    private void UpdateFireballs()
    {
        if (firballs.Count > 0)
        {
            List<GameObject> toRemoveFireball = new List<GameObject>();
            for (int i = 0; i < firballs.Count; i++)
            {
                if (firballs[i].transform.position.x < 35 && firballs[i].transform.position.x > -35)
                {

                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(firballs[i].transform.position, 0.5f, enemyLayers);
                    bool enemyHit = false;
                    foreach (Collider2D collider in hitColliders)
                    {
                        IDamagable damagable = collider.GetComponent<IDamagable>();
                        if (damagable != null)
                        {
                            damagable.TakeDamage(fireDamage + bonusDamage);
                            enemyHit = true;
                        }
                    }

                    if (enemyHit == true)
                    {
                        toRemoveFireball.Add(firballs[i]);
                        firballs[i].SetActive(false);                        
                    }
                }
                else
                {
                    firballs[i].SetActive(false);
                    toRemoveFireball.Add(firballs[i]);                    
                }
            }

            foreach (GameObject fireball in toRemoveFireball)
            {
                shurikens.Remove(fireball);
                Destroy(fireball);
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
                    Shuriken();
                }

                if (Input.GetAxis("FireBall") > 0 && fireCooldownTimer <= 0)
                {
                    FireBall();
                }
            }
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (fireCooldownTimer > 0)
        {
            fireCooldownTimer -= Time.deltaTime;
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

    private void Shuriken()
    {
        if (shurikenAmount > 0)
        {
            shurikenAmount -= 1;

            GameObject shurikenObject = Instantiate(shuriken, attackPoint.position, transform.rotation);

            if (movementScript.IsGrounded())
            {
                shurikenObject.GetComponent<Rigidbody2D>().velocity = new Vector3(movementScript.lastDirection * throwSpeed, 0 * throwSpeed, 90);
            }
            else
            {
                shurikenObject.GetComponent<Rigidbody2D>().velocity = new Vector3(movementScript.lastDirection * throwSpeed, -1 * throwSpeed, 90);
            }
            shurikens.Add(shurikenObject);
            attackCooldownTimer = throwCooldown;
        }
    }

    private void FireBall()
    {

        GameObject fireballObject = Instantiate(fireball, attackPoint.position, transform.rotation);

        if (!movementScript.IsGrounded())
        {
            fireballObject.GetComponent<Rigidbody2D>().velocity = new Vector3(movementScript.lastDirection * fireSpeed, -1 * fireSpeed, 0);
            // Angle it down when in-air
            Quaternion forwardRotation = Quaternion.Euler(0, 0, -45);
            if (movementScript.lastDirection == -1)
            {
                forwardRotation = Quaternion.Euler(0, -180, -45);
            }
            fireballObject.transform.rotation = forwardRotation;
        }
        else
        {
            fireballObject.GetComponent<Rigidbody2D>().velocity = new Vector3(movementScript.lastDirection * fireSpeed, 0 * fireSpeed, 0);
        }

        firballs.Add(fireballObject);
        fireCooldownTimer = fireCooldown;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            health -= damageAmount - defence;
            hud.UpdateHealth();
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
