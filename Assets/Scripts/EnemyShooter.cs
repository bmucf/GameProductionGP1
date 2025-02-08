using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootingInterval = 2f;
    private float shootingTimer;

    private Transform playerTransform;
    public float activationDistance = 10f;
    private bool isActivated = false;

    public int health = 2;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        if (!isActivated && Vector3.Distance(transform.position, playerTransform.position) <= activationDistance)
        {
            isActivated = true;
        }

        if (isActivated)
        {
            RotateTowardsPlayer();

            shootingTimer -= Time.deltaTime;

            if (shootingTimer <= 0f)
            {
                ShootProjectile();
                shootingTimer = shootingInterval;
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);
        }
    }

    void ShootProjectile()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            projectile.GetComponent<Projectile>().SetDirection(direction);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}