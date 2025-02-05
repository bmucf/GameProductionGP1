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
            shootingTimer -= Time.deltaTime;

            if (shootingTimer <= 0f)
            {
                ShootProjectile();
                shootingTimer = shootingInterval;
            }
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
}