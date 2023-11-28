using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Target> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public bool isGameActive;
    public GameObject titleOfScreen;    
    public GameObject pauseScreen;

    private float spawnRate = 1.5f;
    private int score;
    private int lives = 3;
    private bool isPaused;
    
  public void StartGame(int difficulty)
    {      
        spawnRate /= difficulty;
        restartButton.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        isGameActive = true;
        titleOfScreen.SetActive(false);
    }

    void Update()
    {

    }

    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targets.Count);
            Instantiate(targets[randomIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score <= 0)
        {
            score = 0;
        }

        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLives(int subtractLives)
    {
        lives -= subtractLives;

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: " + lives;
    }


}

