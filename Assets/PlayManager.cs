using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    private float startTime;
    private float points;
    public TextMeshProUGUI timeTest;
    public TextMeshProUGUI pointText;

    // Start is called before the first frame update
    void Start()
    {
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
}
