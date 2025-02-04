using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUp : MonoBehaviour
{
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().GainHealth();
            Destroy(this.gameObject);
        }
    }
}