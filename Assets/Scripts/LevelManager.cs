using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public float asteroidSpanRate = 1.0f;

    public Transform asteroid;
    public Transform spawnLocation;
    public Text scoreText;
    public GameObject pauseMenu;

    private bool isSpawning;
    private int score;
    private float asteroidTimer;

    private void Start()
    {
        score = 0;
        asteroidTimer = 0f;
        isSpawning = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isSpawning)
        {
            SpawnAsteroid();
        }
    }


    public void SetScore(int newScore)
    {
        if (scoreText != null)
        {
            score += newScore;
            scoreText.text = score.ToString();
        }
    }

    public void SetSpawn(bool spawn)
    {
        isSpawning = spawn;
    }

    public void Puase()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpuase()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void RestartGameplay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGameplay()
    {
        Debug.Log("Quit to main menu");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    private void SpawnAsteroid()
    {   
        if (asteroidTimer == 0)
        {
            Instantiate(asteroid, spawnLocation.position, spawnLocation.rotation);
            asteroidTimer = asteroidSpanRate;
        }
        else
            asteroidTimer = Mathf.Max(0, asteroidTimer - 0.01f);
    }
}
