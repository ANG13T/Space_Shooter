using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployController : MonoBehaviour
{
    public deployAsteroids[] deployScripts;
    public float spawnWaveTimes = 5f;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnWaves());
    }

    private void increaseDifficulty()
    {
        deployAsteroids script = deployScripts[Random.Range(0, deployScripts.Length)];
        script.respawnTime += 1;
    }

    IEnumerator spawnWaves()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnWaveTimes);
            increaseDifficulty();
        }
    }
}
