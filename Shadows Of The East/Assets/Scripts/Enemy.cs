using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed = 2; //Seconds

    private GameObject player;

    private float attackTime = 0;
    private float attackDistance;

    private void Start()
    {
        attackDistance = Random.Range(0, 3) / 10 + 1f;
        speed = Random.Range(5, 10);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector2 PlayerPosition = player.transform.position;
        Vector2 EnemyPosition = transform.position;

        float distance = Vector2.Distance(PlayerPosition, EnemyPosition);

        if (distance > attackDistance)
            MoveToPlayer();
        else
        {
            attackTime += Time.deltaTime * 1f;

            if (attackTime >= attackSpeed)
            {
                attackTime = 0;
                Hit();
            }
        }

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

    private void Hit()
    {
        print("Hit");
        //attack player
    }
}
