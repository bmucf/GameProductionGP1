using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float movementDelay = 0.5f;
    public float activationDistance = 5f;
    public int health = 8;

    private Transform player;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isWaiting = false;
    private bool isActivated = false;

    public float randomOffsetRange = 1.5f;

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
                targetPosition = player.position + new Vector3(Random.Range(-randomOffsetRange, randomOffsetRange), 0f, Random.Range(-randomOffsetRange, randomOffsetRange));
                StartCoroutine(MoveWithDelay());
            }
        }
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

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();

            if (!playerScript.isInvulnerable)
            {
                playerScript.TakeDamage(1);
            }
        }
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
