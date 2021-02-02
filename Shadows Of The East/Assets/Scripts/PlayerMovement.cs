using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Inputs
    //Player Move
    //Player Dash

    private float PlayerPositionX;
    [SerializeField] private float speed = 20f;
    [Header("Dash settings")]
    [SerializeField] private float dashDistance = 20f;
    [SerializeField] private float dashCooldown = 4f;
    private float dashCooldownTimer = 0;
    private float lastDirection = 1;

    private void Update()
    {
        if (Input.anyKey)
        {
            PlayerPositionX = transform.position.x;
            UpdatePlayerMovement();
            UpdateDash();
            transform.position = new Vector2(PlayerPositionX, transform.position.y);
        }

        UpdateTimers();
    }   

    private void UpdateTimers()
    {
        dashCooldownTimer -= Time.deltaTime;
    }

    private void UpdateDash()
    {   
        if (dashCooldownTimer <= 0 && Input.GetAxis("Dash") > 0)
        {
            PlayerPositionX += lastDirection * dashDistance;

            dashCooldownTimer = dashCooldown;
        }
    }
    private void UpdatePlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            if (horizontal > 0)
            {
                lastDirection = 1;
            }
            else
            {
                lastDirection = -1;
            }
        }

        PlayerPositionX += horizontal * Time.deltaTime * speed;
    }
}
