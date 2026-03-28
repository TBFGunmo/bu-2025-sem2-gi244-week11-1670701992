using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{



    public Transform[] spawnPoints;
    private Transform currentSpawnPoint;
    public int RandomCurrentSpawnPoint;

    public GameObject[] PowerUpPrefabs;
    public int numberOfRandomPowerUp;

    public GameObject enemyPrefab;


    public int totalWave;
    private int currentWave;

    public SpawnData[] spawnDatas;

    bool inSpawn = false;
    bool nextWave = true;

    private Coroutine waveSpawnRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalWave = spawnDatas.Length;
        currentWave = 0;

        RandomPowerUp(spawnDatas[currentWave].numberOfPowerUp);
    }

    // Update is called once per frame
    void Update()
    {
        if ((nextWave && !inSpawn) && !(currentWave >= totalWave))
        {
            nextWave = false;

            if (waveSpawnRoutine != null) 
            {
                StopCoroutine(waveSpawnRoutine);
            }

            waveSpawnRoutine = StartCoroutine(WaveSpawn());
        }
    }

    private void RandomPowerUp(int count) 
    {
        for (int i = 0; i < count; i++)
        {
            RandomCurrentSpawnPoint = Random.Range(0, spawnPoints.Length);
            currentSpawnPoint = spawnPoints[RandomCurrentSpawnPoint];

            numberOfRandomPowerUp = Random.Range(0, PowerUpPrefabs.Length);

            Instantiate(PowerUpPrefabs[numberOfRandomPowerUp], currentSpawnPoint.position + new Vector3(0,1,0), Quaternion.identity);

        }


    }


    IEnumerator WaveSpawn() 
    {
        inSpawn = true;
        RandomPowerUp(spawnDatas[currentWave].numberOfPowerUp);

        yield return new WaitForSeconds(spawnDatas[currentWave].delayStart);
        yield return StartCoroutine(EnemySpawn(spawnDatas[currentWave].totalSpawnEnemies));

        currentWave++;
        nextWave = true;
        Debug.Log("finish wave " + currentWave);

    }

    IEnumerator EnemySpawn(int totalEnemy) 
    {
        for (int i = 0; i < totalEnemy; i++) 
        {
            
            RandomCurrentSpawnPoint = Random.Range(0, spawnDatas[currentWave].numberOfRandomSpawnPoint);
            currentSpawnPoint = spawnPoints[RandomCurrentSpawnPoint];

            Instantiate(enemyPrefab, currentSpawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDatas[currentWave].spawnInterval);
        }
        inSpawn = false;
    }

}
