using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameStats;
    public GameObject gameOverObjects;
    public GameObject cam;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI pointText;

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }



    public void GameOver()
    {
        gameStats.SetActive(false);
        gameOverObjects.SetActive(true);
        Debug.Log("Game Over!!!");
        float finalTime = cam.GetComponent<PlayManager>().getTime();
        float finalPoints = cam.GetComponent<PlayManager>().getPoints();
        timeText.SetText("Time: " + finalTime.ToString("F2"));
        pointText.SetText("Points: " + finalPoints.ToString());
        Time.timeScale = 0f;
    }

    public void playAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
