using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask wallLayer;
    public float activationDistance = 10f;
    public float chargeCooldown = 1f;

    private Transform player;
    private Vector3 chargeDirection;
    private bool isCharging = false;
    private bool hasActivated = false;
    private float initialY;

    public int health = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialY = transform.position.y;
    }

    void Update()
    {
        if (!hasActivated && Vector3.Distance(transform.position, player.position) <= activationDistance)
        {
            hasActivated = true;
            isCharging = true;
            chargeDirection = (player.position - transform.position).normalized;
        }

        if (isCharging)
        {
            Charge();
        }
        else
        {
            RotateTowardsPlayer();
        }
    }

    void Charge()
    {
        Vector3 newPosition = transform.position + chargeDirection * moveSpeed * Time.deltaTime;
        newPosition.y = initialY;
        transform.position = newPosition;

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

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
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

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        LoadScene("WinScreen");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}