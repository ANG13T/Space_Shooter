using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deployAsteroids : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public string mode = "left";

    // Use this for initialization
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(asteroidWave());
    }
    private void spawnEnemy()
    {
        Debug.Log("Spawing enemy");
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject a = Instantiate(asteroidPrefab) as GameObject;

        Vector3 range = new Vector3(0, 0, 0);

        switch (mode)
        {
            case "left":
                range = new Vector3(-10, Random.Range(-screenBounds.y, screenBounds.y), -2);
                break;

            case "right":
                range = new Vector3(screenBounds.x + 10, Random.Range(-screenBounds.y, screenBounds.y), -2);
                break;

            case "top":
                range = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), -10, -2);
                break;

            case "bottom":
                range = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.x + 10, -2);
                break;
        }

        a.gameObject.GetComponent<Asteroid>().type = mode;
        a.transform.position = range;
        Debug.Log(asteroidPrefab.gameObject.name);
        Debug.Log(mode);
    }


    IEnumerator asteroidWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
