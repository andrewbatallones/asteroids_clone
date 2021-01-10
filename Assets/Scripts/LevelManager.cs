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
    public Text levelText;
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject pauseMenu;
    public Transform[] spawnLocations;
    public Slider expBar;

    private bool isSpawning;
    private int level;
    private float exp;
    private float maxExp;
    private int score;
    private float asteroidTimer;

    private void Start()
    {
        score = 0;
        level = 1;
        asteroidTimer = 0f;
        isSpawning = true;

        SetLevelData();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isSpawning)
        {
            SpawnAsteroid();
        }

        if (exp >= maxExp)
            LevelUp();
    }


    public void SetScore(int newScore)
    {
        if (levelText != null)
        {
            score += newScore;
            exp += newScore;
            expBar.value = exp;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

    private void SpawnAsteroid()
    {
        if (asteroidTimer == 0 || !AsteroidExists())
        {
            int index = Random.Range(0, spawnLocations.Length);
            
            Instantiate(asteroid, new Vector3(spawnLocations[index].position.x, spawnLocations[index].position.y -10), spawnLocations[index].rotation);
            asteroidTimer = (asteroidSpanRate / level) * 10;
        }
        else
            asteroidTimer = Mathf.Max(0, (asteroidTimer - 0.01f) * Time.deltaTime);
    }

    private void SetLevelData()
    {
        exp = 0f;
        maxExp = GetMaxExp();
        expBar.value = Mathf.Max(0, (exp - maxExp));
        expBar.maxValue = maxExp;
    }

    private void LevelUp()
    {
        level += 1;
        levelText.text = level.ToString();
        SetLevelData();
    }

    private float GetMaxExp()
    {
        return 100 * level;
    }

    private bool AsteroidExists()
    {
        return (GameObject.Find("Asteroid(Clone)") != null);
    }
}
