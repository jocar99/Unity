using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject livesText;
    public int score = 0;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateUI()
    {
        if (lives == 0) Debug.Log("Game Over");
        if (lives < 0) lives = 0;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score = " + score;
        livesText.GetComponent<TextMeshProUGUI>().text = "Lives = " + lives;
    }
}
