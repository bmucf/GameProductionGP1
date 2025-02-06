using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask wallLayer;
    public float activationDistance = 10f;
    public float chargeCooldown = 1f;

    private Transform player;
    private Vector3 chargeDirection;
    private bool isCharging = false;

    public int health = 20; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isCharging && Vector3.Distance(transform.position, player.position) <= activationDistance)
        {
            isCharging = true;
            chargeDirection = (player.position - transform.position).normalized;
        }

        if (isCharging)
        {
            Charge();
        }
    }

    void Charge()
    {
        transform.Translate(chargeDirection * moveSpeed * Time.deltaTime);

        if (Physics.Raycast(transform.position, chargeDirection, 1f, wallLayer))
        {
            isCharging = false;
            StartCoroutine(TurnToPlayerAndChargeAgain());
        }
    }

    System.Collections.IEnumerator TurnToPlayerAndChargeAgain()
    {
        yield return new WaitForSeconds(chargeCooldown);

        chargeDirection = (player.position - transform.position).normalized;

        isCharging = true;
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