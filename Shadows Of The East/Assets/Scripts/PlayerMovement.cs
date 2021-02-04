using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float PlayerPositionX;
    private Rigidbody2D rigidBody;
    private Animator animator;

    [Header("Stamina Settings")]
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private int staminaPerSecond = 5;
    public int stamina;
    [Header("Movement settings")]
    [SerializeField] private float speed = 20f;
    [Header("Dash settings")]
    [SerializeField] private float dashDistance = 20f;
    [SerializeField] private float dashCooldown = 4f;
    [SerializeField] private int dashStaminaCost = 25;    
    private float dashCooldownTimer = 0;
    public float lastDirection { private set; get; } = 1;
    [Header("Jump settings")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform jumpCheckPoint;
    private float jumpBuffer = 0;
    private float extraSpeed = 0;
    public int staminaReduction { private set; get; } = 0;
    private float staminaRechargeTimer;
    
    public bool IsGrounded()
    {
        return Physics2D.Raycast(jumpCheckPoint.transform.position, Vector3.down, 0.2f, groundLayers);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
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
        transform.position = new Vector2(Mathf.Clamp(PlayerPositionX, (float)-8.5, (float)8.5), transform.position.y);
    }   

    private void UpdateTimers()
    {
        dashCooldownTimer -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;

        if (stamina < maxStamina)
        {
            staminaRechargeTimer += Time.deltaTime;
            if (staminaRechargeTimer >= 1f)
            {
                stamina += staminaPerSecond;
                Mathf.Clamp(stamina, 0, maxStamina);
                staminaRechargeTimer = 0;
            }
        }
        else
        {
            staminaRechargeTimer = 0;
        }
    }

    private void UpdateJump()
    {
        if (Input.GetAxis("Jump") > 0 )
        {
            jumpBuffer = 0.2f;
        }

        if (jumpBuffer > 0)
        {
            if (IsGrounded())
            {
                animator.SetFloat("CharacterState", 4);
                jumpBuffer = 0;
                rigidBody.velocity = Vector2.up * jumpForce;
            }
            else
            {
                if (animator.GetFloat("CharacterState") == 4)
                {
                    animator.SetFloat("CharacterState", 1);
                }
            }
        }
    }

    private void UpdateDash()
    {   
        if (dashCooldownTimer <= 0 && Input.GetAxis("Dash") > 0 && EnoughStaminaToDash())
        {
            PlayerPositionX += lastDirection * dashDistance;

            dashCooldownTimer = dashCooldown;
            stamina -= dashStaminaCost;
        }
    }

    private bool EnoughStaminaToDash()
    {
        int dashCost = dashStaminaCost - staminaReduction;
        if (stamina >= dashCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void UpdatePlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            animator.SetFloat("CharacterState", 3);
            if (horizontal > 0)
            {
                lastDirection = 1;
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                lastDirection = -1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        PlayerPositionX += horizontal * Time.deltaTime * (speed + extraSpeed);
    }


    public void AddMovementSpeed(float duration, float speed, int staminaReduction)
    {
        extraSpeed += speed;
        staminaReduction += staminaReduction;
        StartCoroutine(RemoveBonusSpeed(duration, speed, staminaReduction));
    }
    

    IEnumerator RemoveBonusSpeed(float duration, float speed, float staminaReduction)
    {
        yield return new WaitForSeconds(duration);

        extraSpeed -= speed;
        staminaReduction -= staminaReduction;
    }
}
