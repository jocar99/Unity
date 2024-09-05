using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    private int score;
    private AudioSource musicSource;
    private bool gamePaused;

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public int lives;
    public Slider soundSlider;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = SaveSettings.volume;
        soundSlider.value = SaveSettings.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!gamePaused && isGameActive)
            {
                Time.timeScale = 0.0f;
                gamePaused = true;
                isGameActive = false;
                pauseScreen.SetActive(true);
            }
            else if (gamePaused && !isGameActive)
            {
                Time.timeScale = 1.0f;
                gamePaused = false;
                isGameActive = true;
                pauseScreen.SetActive(false);
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        SaveSettings.volume = musicSource.volume;
        gamePaused = false;
        lives = 3;
        livesText.text = "Lives: " + lives;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    public void UpdateSound()
    {
        musicSource.volume = soundSlider.value;
    }
}
