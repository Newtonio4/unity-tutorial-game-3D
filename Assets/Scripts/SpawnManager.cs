using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs1;
    public GameObject[] obstaclePrefabs2;
    public GameObject[] obstaclePrefabs3;
    public GameObject[] obstaclePrefabs4;
    public GameObject[] enemyPrefabs;

    private GameManager gameManager;
    private float startDelay = 3;
    private float repeatRate = 1f;
    private float waveDuration = 15;
    private float enemyHeightOffset = 0;
    private float waveNum = 0;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void StartWave()
    {
        float rate = repeatRate / (1 + (gameManager.difficulty - 1) * 0.5f);

        InvokeRepeating("SpawnObstacle", startDelay, rate);
        InvokeRepeating("SpawnEnemy", startDelay, rate * 2);
        MakeEnemyWave();
        StartCoroutine(Disable());
    }

    void SpawnObstacle()
    {
        var random = Random.Range(1, 5);
        GameObject obstacle;

        if (random == 1)
        {
            obstacle = obstaclePrefabs1[Random.Range(0, obstaclePrefabs1.Length)];
            Instantiate(obstacle, new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, Random.Range(-25, 26)), obstacle.transform.rotation);
        }
        else if (random == 2)
        {
            obstacle = obstaclePrefabs2[Random.Range(0, obstaclePrefabs1.Length)];
            Instantiate(obstacle, new Vector3(obstacle.transform.position.x, obstacle.transform.position.y, Random.Range(-25, 26)), obstacle.transform.rotation);
        }
        else if (random == 3)
        {
            obstacle = obstaclePrefabs3[Random.Range(0, obstaclePrefabs1.Length)];
            Instantiate(obstacle, new Vector3(Random.Range(-25, 26), obstacle.transform.position.y, obstacle.transform.position.z), obstacle.transform.rotation);
        }
        else if (random == 4)
        {
            obstacle = obstaclePrefabs4[Random.Range(0, obstaclePrefabs1.Length)];
            Instantiate(obstacle, new Vector3(Random.Range(-25, 26), obstacle.transform.position.y, obstacle.transform.position.z), obstacle.transform.rotation);
        }
    }

    void SpawnEnemy()
    {
        var random = Random.Range(0, 4);
        Vector3 pos = new Vector3(0, 0, 0);

        var enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];


        if (random == 0)
        {
            pos = new Vector3(26, enemyHeightOffset, Random.Range(-25, 26));
        }
        else if (random == 1)
        {
            pos = new Vector3(-26, enemyHeightOffset, Random.Range(-25, 26));
        }
        else if (random == 2)
        {
            pos = new Vector3(Random.Range(-25, 26), enemyHeightOffset, 26);
        }
        else if (random == 3)
        {
            pos = new Vector3(Random.Range(-25, 26), enemyHeightOffset, -26);
        }

        Instantiate(enemyPrefab, pos, enemyPrefab.transform.rotation);
    }

    void MakeEnemyWave()
    {
        for (int i = 0; i < waveNum * gameManager.difficulty * 3; i++)
            SpawnEnemy();

        if (waveNum < 5)
            waveNum++;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(waveDuration);
        CancelInvoke();
        if (repeatRate > 0.5f)
            repeatRate -= 0.1f;

        StartWave();
    }
}
