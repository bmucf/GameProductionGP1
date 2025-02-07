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
    public TextMeshProUGUI bowPickUpText;
    public GameObject bowPickUp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPText.text = "HP: " + GameObject.Find("Player").GetComponent<Player>().health;
        if (bowPickUpText != null)
        {
            bowPickUpText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (player.GetComponent<Player>().health <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }

        if (bowPickUpText != null)
        {
            MakeBowText();
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

    void MakeBowText()
    {
        if (bowPickUp == null)
        {
            return;
        }

        if (bowPickUp.GetComponent<BowPickUp>().isActivated == true)
        {
            Debug.Log("Bow is Activated");

            if (bowPickUpText != null)
            {
                Debug.Log("Text is Created");
                bowPickUpText.text = "YOU GOT THE CROSSBOW";
                Invoke("DeleteText", 2f);
            }
        }
    }

    void DeleteText()
    {
        if (bowPickUpText != null)
        {
            bowPickUpText.text = "";
        }

    }
}
