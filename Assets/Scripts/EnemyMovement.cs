using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gridSize = 1f;
    public float movementDelay = 0.5f;
    public float activationDistance = 5f;
    public int health = 8;

    private Transform player;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isWaiting = false;
    private bool isActivated = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isActivated)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            
            if (distanceToPlayer <= activationDistance)
            {
                isActivated = true;
                StartCoroutine(MoveWithDelay());
            }
        }
        else
        {
            if (!isMoving && !isWaiting)
            {
                targetPosition = GetNearestGridPosition(player.position);
                StartCoroutine(MoveWithDelay());
            }
        }
    }

    Vector3 GetNearestGridPosition(Vector3 position)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;
        return new Vector3(x, transform.position.y, z);
    }

    IEnumerator MoveWithDelay()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(movementDelay);

        isMoving = false;
        isWaiting = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
