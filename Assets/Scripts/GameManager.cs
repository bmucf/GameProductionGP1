using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI HPText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPText.text = "HP: " + GameObject.Find("Player").GetComponent<Player>().health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void HealthText(float currentHealth)
    {
        HPText.text = "HP: " + currentHealth;
    }

    void QuitGame()
    {
        Debug.Log("Exiting the game!");
        Application.Quit();
    }
}
