using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scaleText;
    public TextMeshProUGUI resultText;
    public GameObject menuScreen;
    public GameObject gameOverScreen;
    public int difficulty = 1;
    public bool isGameActive = false;

    private SpawnManager spawnManager;
    private int time;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        time = 0;
        UpdateScore(0);
    }

    public void StartGame(int newDifficulty)
    {
        isGameActive = true;
        difficulty = newDifficulty;
        timeText.gameObject.SetActive(true);
        scaleText.gameObject.SetActive(true);
        menuScreen.SetActive(false);
        spawnManager.StartWave();
        StartCoroutine(CountTime());
    }

    public void GameOver()
    {
        resultText.text = "Youre time: " + time;
        isGameActive = false;
        gameOverScreen.SetActive(true);
        timeText.gameObject.SetActive(false);
        scaleText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (isGameActive)
                UpdateScore(1);
        }
    }

    private void UpdateScore(int timeToAdd)
    {
        time += timeToAdd;
        timeText.text = "Time: " + time + " s";
    }
}
