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

    private void Update()
    {
        if (Input.anyKey)
        {
            UpdatePlayerMovement();
        }
    }

    private void UpdatePlayerMovement()
    {
        PlayerPositionX += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.position = new Vector2(PlayerPositionX, transform.position.y);
    }
}
