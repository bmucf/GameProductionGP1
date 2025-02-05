using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arrow : MonoBehaviour
{
    public float speed = 8f;
    public LayerMask Enemy;
    public LayerMask Pot;
    public int enemyDMG = 2; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        int otherLayer = other.gameObject.layer;
        string layerName = LayerMask.LayerToName(otherLayer);

        if (((1 << otherLayer) & Enemy) != 0)
        {
            if (other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                enemy.TakeDamage(enemyDMG);
                Debug.Log("Arrow hit enemy and did " + enemyDMG + "damage");
            }
            Destroy(gameObject);
        }

        else if (((1 << otherLayer) & Pot) != 0)
        {
            if (other.gameObject.TryGetComponent<BreakableObject>(out BreakableObject pot))
            {
                pot.Break();
                Debug.Log("Arrow hit pot and destroyed it");
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
