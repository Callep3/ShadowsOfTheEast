using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private Vector3 targetPos;
    private void Awake()
    {
        if (transform.position.x > 0)
        {
            targetPos = new Vector3(-28, transform.position.y);
        }
        else
        {
            targetPos = new Vector3(28, transform.position.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        float y = targetPos.y;
        y += Random.Range(-1f, 1f);
        y = Mathf.Clamp(y,minY,maxY);
        targetPos.y = y;

        transform.position = Vector3.Lerp(transform.position, targetPos, movementSpeed * Time.fixedDeltaTime);

        if (targetPos.x > 0)
        {
            if (transform.position.x > 25)
            {
                GhostSpawnManager.Instance.numberOfActiveGhosts--;
                Destroy(gameObject);
            }            
        }
        else if (targetPos.x < 0)
        {
            if (transform.position.x < -25)
            {
                GhostSpawnManager.Instance.numberOfActiveGhosts--;
                Destroy(gameObject);
            }
        }
    }
}
