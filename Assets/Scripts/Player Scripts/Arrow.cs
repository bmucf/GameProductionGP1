using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arrow : MonoBehaviour
{
    public float speed = 8f;
    public LayerMask Enemy;
    public LayerMask Pot;
    public LayerMask Target;
    public int enemyDMG = 2; 

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        int otherLayer = other.gameObject.layer;
        string layerName = LayerMask.LayerToName(otherLayer);

        // Check if it hit an enemy
        if (((1 << otherLayer) & Enemy) != 0)
        {
            if (other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemy))
            {
                // Apply damage to the enemy
                enemy.TakeDamage(enemyDMG);
                Debug.Log("Arrow hit enemy and did " + enemyDMG + " damage");
            }
            Destroy(gameObject);
        }

        if (((1 << otherLayer) & Enemy) != 0)
        {
            if (other.gameObject.TryGetComponent<EnemyShooter>(out EnemyShooter enemyShooter))
            {
                // Apply damage to the enemy
                enemyShooter.TakeDamage(enemyDMG);
                Debug.Log("Arrow hit enemy and did " + enemyDMG + " damage");
            }
            Destroy(gameObject);
        }

        if (((1 << otherLayer) & Enemy) != 0)
        {
            if (other.gameObject.TryGetComponent<Boss>(out Boss boss))
            {
                // Apply damage to the enemy
                boss.TakeDamage(enemyDMG);
                Debug.Log("Arrow hit enemy and did " + enemyDMG + " damage");
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {

        int otherLayer = other.gameObject.layer;
        string layerName = LayerMask.LayerToName(otherLayer);
        // Check if it hit a pot
        if (((1 << otherLayer) & Pot) != 0)
        {
            if (other.gameObject.TryGetComponent<BreakableObject>(out BreakableObject pot))
            {
                pot.Break();
                Debug.Log("Arrow hit pot and destroyed it");
            }
            Destroy(gameObject);
        }

        // Check if it hit a target
        else if (((1 << otherLayer) & Target) != 0)
        {
            if (other.gameObject.TryGetComponent<Target>(out Target target))
            {
                target.Activate();
                Debug.Log("Arrow hit target and activated it");
            }
            Destroy(gameObject);
        }

        
        else
        {
            Destroy(gameObject);
        }
    }
}