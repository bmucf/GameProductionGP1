using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BowPickUp : MonoBehaviour
{
    public TextMeshProUGUI bowPickUpText;
    public bool isActivated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<Player>().hasCrossbow = true;

            isActivated = true;
            Destroy(gameObject, 0.1f);

        }
    }

   
}
