using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float devider;

    void Update()
    {
        float PlayerPosition = player.transform.position.x;
        if (PlayerPosition > 0)
        {
            transform.position = new Vector3(-player.transform.position.x / devider, 0, 0);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x / -devider, 0, 0);
        }
    }
}
