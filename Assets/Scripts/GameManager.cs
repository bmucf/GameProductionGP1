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
        HPText.text = "HPText: 3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
