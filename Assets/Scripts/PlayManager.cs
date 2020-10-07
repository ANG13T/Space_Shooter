using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    private float startTime;
    private float points;
    public GameObject eventManager;
    public TextMeshProUGUI timeTest;
    public TextMeshProUGUI pointText;
    public GameObject playStats;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        playStats.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        InvokeRepeating("AddTimePoints", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        timeTest.SetText("Time: " + startTime.ToString("F2"));
    }

    private void AddTimePoints()
    {
        addPoints(1);
    }

    public void addPoints(float amount)
    {
        points += amount;
        pointText.SetText("Points: " + points.ToString());
    }

    public float getPoints()
    {
        return points;
    }

    public float getTime()
    {
        return startTime;
    }

}
