using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed = 2; //Seconds
    [SerializeField] private Vector3 boxOffset;
    [SerializeField] private Vector3 Size;

    private GameObject player;

    [SerializeField] private int attackDamage = 2;
    private float hitCooldown = 0;
    private float attackTime = 0;
    private float attackDistance;
    private float maxHealth = 3;
    private float currentHealth = 0;
    private int facing;

    private void Start()
    {
        currentHealth = maxHealth;
        attackDistance = Random.Range(0, 3) / 10 + 1f;
        speed = Random.Range(5, 10);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector2 PlayerPosition = player.transform.position;
        Vector2 EnemyPosition = transform.position;
        float distance = Vector2.Distance(PlayerPosition, EnemyPosition);


        // which way it's facing
        if (PlayerPosition.x > EnemyPosition.x)
            facing = -1;
        else
            facing = 1;


        // Attack & movement
        if (distance > attackDistance)
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

    private void MoveToPlayer()
    {
        Vector2 PlayerPosition = player.transform.position;
        Vector2 EnemyPosition = transform.position;

        float distance = Vector2.Distance(PlayerPosition, EnemyPosition);
        float direction;

        if (player.transform.position.x - attackDistance > transform.position.x)
            direction = 1;
        else if (player.transform.position.x + attackDistance < transform.position.x)
            direction = -1;
        else
            direction = 0;

        if (direction != 0)
        {
            float realSpeed = direction * (Time.deltaTime * (speed / 10 + 1));
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
            currentHealth -= Damage;
        else
            //aniamtion
            Destroy(gameObject);
    }


    private void Attack()
    {
        //attack player
        Vector3 facingAttack;

        if (facing == 1)
            facingAttack = new Vector3(0.53f, 0, 0);
        else if (facing == -1)
            facingAttack = new Vector3(-0.54f, 0, 0);
        else
            return;

        RaycastHit2D hit2D;
        hit2D = Physics2D.BoxCast(transform.position + facingAttack, new Vector3(0.64f,1,0), 0, Vector2.zero, 0);
        if (hit2D.collider != null)
            if (hit2D.collider.CompareTag("Player"))
            {
                hit2D.collider.GetComponent<IDamagable>().TakeDamage(attackDamage);
            }
    }
}
