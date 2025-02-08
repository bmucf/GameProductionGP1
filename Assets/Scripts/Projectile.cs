using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    private float timer;

    private float fixedY;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        fixedY = transform.position.y;
    }

    void Update()
    {
        Vector3 movement = direction * speed * Time.deltaTime;
        movement.y = 0;

        transform.Translate(movement, Space.World);

        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);

        timer += Time.deltaTime;

        if(timer > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (player.GetComponent<Player>().isBlocking == false)
                player.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}