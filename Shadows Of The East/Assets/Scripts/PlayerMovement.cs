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
    private Rigidbody2D rigidBody;

    [Header("Movement settings")]
    [SerializeField] private float speed = 20f;
    [Header("Dash settings")]
    [SerializeField] private float dashDistance = 20f;
    [SerializeField] private float dashCooldown = 4f;
    private float dashCooldownTimer = 0;
    private float lastDirection = 1;
    [Header("Jump settings")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private LayerMask groundLayers;
    private float jumpBuffer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerPositionX = transform.position.x;

        if (Input.anyKey)
        {
            UpdatePlayerMovement();
            UpdateDash();
        }
        UpdateJump();

        UpdateTimers();
        transform.position = new Vector2(PlayerPositionX, transform.position.y);
    }   

    private void UpdateTimers()
    {
        dashCooldownTimer -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;
    }

    private void UpdateJump()
    {
        if (Input.GetAxis("Jump") > 0 )
        {
            jumpBuffer = 0.2f;
        }

        if (jumpBuffer > 0)
        {
            if (Physics2D.Raycast(transform.position, Vector3.down, 0.7f, groundLayers))
            {
                jumpBuffer = 0;
                rigidBody.velocity = Vector2.up * jumpForce;
            }
        }
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
