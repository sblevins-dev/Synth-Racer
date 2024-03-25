using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    [SerializeField] GameObject menu;
    [SerializeField] GameObject gameInterface;
    [SerializeField] GameObject spawnManager;
    [SerializeField] TextMeshProUGUI clock;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI gameOverTimeText;
    [SerializeField] GameObject gameOver;
    LifeBar lifebar;
    public bool isGameOver = false;
    public bool isGameRunning = false;
    float time = 0.0f;

    public int hitPoints = 3;
    int maxHitPoints = 6;

    public float respawnRate;
    public int difficulty;

    public int score = 0;

    public float playerSpeed = 20f;
    [SerializeField] float speedIncrease = 20f;
    public float speed = 20f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        menu.SetActive(true);
        lifebar = GameObject.FindGameObjectWithTag("LifeBar").GetComponent<LifeBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && isGameRunning)
        {
            ClockHandler();
        }
    }

    public void AddHitPoints()
    {
        hitPoints++;
        lifebar.UpdateLifeBar();
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
            GameOver();

        lifebar.UpdateLifeBar();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        if (hitPoints <= maxHitPoints && score % 50 == 0 && score != 0)
        {
            AddHitPoints();
        }
    }

    IEnumerator IncreaseSpeed()
    {
        while (isGameRunning && !isGameOver)
        {
            yield return new WaitForSeconds(10f);
            playerSpeed += speedIncrease;
        }
        
    }

    void CancelCoroutine()
    {
        StopCoroutine(ReduceSpeed(0));
    }

    IEnumerator ReduceSpeed(float initialSpeed)
    {
        int count = 10;

        while (count-- > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        playerSpeed = initialSpeed;
        CancelCoroutine();
    }

    public void ReduceSpeedHandler(float initialSpeed)
    {
            StartCoroutine(ReduceSpeed(initialSpeed));
    }

    void CountTime()
    {
        time += Time.deltaTime;
    }

    void ClockHandler()
    {
        CountTime();

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        clock.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            // Easy
            case 0:
                this.difficulty = difficulty;
                break;
            // Medium
            case 1:
                this.difficulty = difficulty;
                playerSpeed = 200f;
                break;
            // Hard
            case 2:
                this.difficulty = difficulty;
                playerSpeed = 400f;
                break;
            default: 
                this.difficulty = 0;
                break;
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
        menu.SetActive(false);
        spawnManager.SetActive(true);
        gameInterface.SetActive(true);

        StartCoroutine(IncreaseSpeed());
    }

    public void RestartGame()
    {
        playerSpeed = 20f;
        time = 0.0f;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameOver = true;
        isGameRunning = false;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        gameOverTimeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

        //gameOverTimeText.text = "Time: " + time;
        gameOverScoreText.text = "Score: " + score;

        gameInterface.SetActive(false);
        StopAllCoroutines();
    }
}
