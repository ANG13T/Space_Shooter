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
    public TextMeshProUGUI ammoText;
    public int ammoCapacity;
    private int ammoAmount;
    public GameObject playStats;
    public GameObject reloadContainer;
    public Slider staminaSlider;
    public Slider reloadingSlider;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private int maxStamia = 100;
    private int reloadAmount;
    private int currentStamina;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    // Start is called before the first frame update
    void Start()
    {
        reloadContainer.SetActive(false);
        playStats.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        InvokeRepeating("AddTimePoints", 1f, 1f);
        currentStamina = maxStamia;
        staminaSlider.maxValue = maxStamia;
        staminaSlider.value = maxStamia;
        reloadingSlider.value = 0;
        reloadingSlider.maxValue = 100;
        ammoAmount = ammoCapacity;
        updateAmmoText();


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

    public void addReload(int amount)
    {
        reloadAmount += amount;
        if (reloadAmount >= 100)
        {
            reloadContainer.SetActive(false);
            reloadingSlider.value = 0;
            reloadAmount = 0;
            ammoAmount = ammoCapacity;
            updateAmmoText();
        }
        else
        {
            reloadingSlider.value = reloadAmount;
        }
        
    }

    public float getPoints()
    {
        return points;
    }

    public float getTime()
    {
        return startTime;
    }

    public void showReloadScreen()
    {
        reloadContainer.SetActive(true);
    }

    private void updateAmmoText()
    {
        ammoText.SetText(ammoAmount.ToString() + " / " + ammoCapacity.ToString());
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

    public bool hasAmmo()
    {
        return ammoAmount > 0;
    }

    public void useAmmo(int amount)
    {
        ammoAmount -= amount;
        updateAmmoText();
        if (!hasAmmo())
        {
            reloadAmount = 0;
            reloadingSlider.value = 0;
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
