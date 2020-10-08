using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayManager : MonoBehaviour
{
    private float startTime;
    private float points;
    public GameObject eventManager;
    public TextMeshProUGUI timeTest;
    public TextMeshProUGUI pointText;
    public GameObject playStats;
    public Slider staminaSlider;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private int maxStamia = 100;
    private int currentStamina;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    // Start is called before the first frame update
    void Start()
    {
        playStats.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        InvokeRepeating("AddTimePoints", 1f, 1f);
        currentStamina = maxStamia;
        staminaSlider.maxValue = maxStamia;
        staminaSlider.value = maxStamia;
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

    public void useStamina(int amount)
    {
        Debug.Log("Using Stamina: " + amount.ToString());
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaSlider.value = currentStamina;

            if(regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
    }

    public float getStamina() {
        return currentStamina;
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);

        while(currentStamina < maxStamia)
        {
            currentStamina += maxStamia / 100;
            staminaSlider.value = currentStamina;
            yield return regenTick;
        }

        regen = null;
    }

}
