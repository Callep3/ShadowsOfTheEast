using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour, IDamagable
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float attackSpeed = 1.75f; //Seconds
    [SerializeField] private float powerUpDropYOffset = 2f;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private EnemySound soundScript;
    [SerializeField] private Vector2 boxOffset;
    [SerializeField] private Vector2 boxSize;

    private Animator animator;
    private Rigidbody2D rb;
    private float torque;

    private GameObject player;

    [SerializeField] private int attackDamage = 6;
    private float hitCooldown = 0;
    private float attackTime = 1;
    [SerializeField] private float maxHealth = 9;
    private float currentHealth = 0;
    private int facing;

    private bool isVisible;
    private bool dead;

    private void Start()
    {
        EnemyScaling();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        torque = Random.Range(-180, 180);

        animator = GetComponent<Animator>();
        int random = Random.Range(0, 2);
        Debug.Log(random);
    }

    private void Update()
    {
        // which way it's facing
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facing = -1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facing = 1;
        }


        // Attack & movement
        if (player.transform.position.x - transform.position.x > attackDistance || transform.position.x - player.transform.position.x > attackDistance)
            MoveToPlayer();
        else
        {
            attackTime += Time.deltaTime * 1f;

            if (attackTime >= attackSpeed)
            {
                attackTime = 0;
                Attack();
            }
        }


        // Hit
        if (hitCooldown < 1)
            hitCooldown += Time.deltaTime * 1f;

    }

    private void EnemyScaling()
    {
        maxHealth *= GameManager.Instance.wave;
        attackDamage *= GameManager.Instance.wave;
    }

    private void MoveToPlayer()
    {
        Vector2 PlayerPosition = player.transform.position;
        Vector2 EnemyPosition = transform.position;

        float distance = Vector2.Distance(PlayerPosition, EnemyPosition);
        float direction;

        if (dead)
            return;

        if (player.transform.position.x - attackDistance > transform.position.x)
            direction = 1;
        else if (player.transform.position.x + attackDistance < transform.position.x)
            direction = -1;
        else
            direction = 0;

        if (direction != 0)
        {
            float realSpeed = direction * (Time.deltaTime * speed);
            if (distance > attackDistance && distance < attackDistance + 0.7f)
                transform.position = new Vector2(transform.position.x + realSpeed / 1.5f, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x + realSpeed, transform.position.y);
        }
    }

    public void TakeDamage(int Damage)
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth - Damage > 0)
        {
            currentHealth -= Damage;
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<SpriteRenderer>().DOColor(Color.white, 0.2f);
            soundScript.GotHit();
        }
        else
        {
            //aniamtion
            Die();
            soundScript.Died();
        }
    }

    private void Die()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GameObject powerup = PowerupManager.Instance.GetDrop();
        if (powerup != null)
        {
            Instantiate(powerup, transform.position + (Vector3.up * powerUpDropYOffset), Quaternion.identity);
        }

        if (dead == false)
        {
            ScoreManager.Instance.IncreaseCombo();
            ScoreManager.Instance.IncreaseScore();
            SpawnManager.Instance.numberOfEnemies--;
            GameManager.Instance.UpdateEnemiesLeftText();
            GetComponent<SpriteRenderer>().DOColor(new Color(0, 0, 0), 0.65f);
            GetComponent<SpriteRenderer>().DOFade(0, 0.7f);
        }

        if (SpawnManager.Instance.numberOfEnemies <= 0)
        {
            GameManager.Instance.NextWave();
            if (GameManager.Instance.doneSpawning)
            {
                GameManager.Instance.doneSpawning = false;
            }
        }

        ScoreManager.Instance.ShakeCamera();

        DeathAnimation();

        StartCoroutine(UntilDestroyed());
    }

    private void DeathAnimation()
    {
        dead = true;
        Vector2 PlayerPosition = player.transform.position;
        Vector2 EnemyPosition = transform.position;

        rb.constraints = RigidbodyConstraints2D.None;
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<CircleCollider2D>());

        if (PlayerPosition.x > EnemyPosition.x)
        {
            //negative force
            rb.AddForce(new Vector2(Random.Range(-10, 0), Random.Range(3, 10)), ForceMode2D.Impulse);
            rb.AddTorque(torque, ForceMode2D.Force);
        }
        else
        {
            //positive force
            rb.AddForce(new Vector2(Random.Range(0, 10), Random.Range(3, 10)), ForceMode2D.Impulse);
            rb.AddTorque(torque, ForceMode2D.Force);
        }
    }

    private IEnumerator UntilDestroyed()
    {
        //yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Attack()
    {
        Vector3 facingAttack;

        if (facing == 1)
            facingAttack = new Vector2(-boxOffset.x, boxOffset.y);
        else if (facing == -1)
            facingAttack = new Vector2(boxOffset.x, boxOffset.y);
        else
            return;

        soundScript.Attacked();
        animator.SetTrigger("Attack");

        StartCoroutine(AttackAtFrame(facingAttack));
    }

    private IEnumerator AttackAtFrame(Vector3 facingAttack)
    {
        yield return new WaitForSeconds(1.5f); 
        RaycastHit2D[] hit2D = Physics2D.BoxCastAll(new Vector2(transform.position.x, transform.position.y) + (Vector2)facingAttack, boxSize, 0f, new Vector2(0,0));
        foreach (RaycastHit2D rayhit in hit2D)
        {
            if (rayhit.collider.CompareTag("Player"))
            {
                rayhit.collider.GetComponent<IDamagable>().TakeDamage(attackDamage);
            }
        }
    }
}
