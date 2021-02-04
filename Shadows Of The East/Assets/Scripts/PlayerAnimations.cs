using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerCombat combatScript;
    [SerializeField] private PlayerMovement movementScript;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = 0;
        if (Input.anyKey)
        {
            horizontal = Input.GetAxis("Horizontal");
        }

        if (horizontal != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        animator.SetBool("Grounded", movementScript.IsGrounded());
    }

 
    public void OnJump()
    {
        animator.SetTrigger("Jump");
    }
    
    public void OnLightAttack()
    {
        animator.SetTrigger("LightAttack");
    }

    public void OnDash()
    {
        animator.SetTrigger("Dash");
    }

}
