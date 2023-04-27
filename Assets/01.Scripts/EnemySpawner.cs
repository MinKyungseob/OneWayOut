using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    public GameObject enemyPrefab;
    public float spawnTime = 0f;
    public int maxEnemy = 5;
    public int enemyCount;
    bool isGameOver;
    bool isSpawned;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("SpawningPoints").GetComponentsInChildren<Transform>(spawnPoints);
        spawnPoints.RemoveAt(0);//부모 자신의 Transform 정보를 제외시킨다.
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        isGameOver = GameManager.Instance.isGameOver;
        maxEnemy = Random.Range(1, maxEnemy);

        if (isGameOver == false)
        {
            for (enemyCount=0; enemyCount < maxEnemy+1; enemyCount++)
            {
                int index = Random.Range(0, spawnPoints.Count);
                Instantiate(enemyPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
                isSpawned = true;
            }

        }
        if (NextRoom.trip == true || BeforeRoom.trip == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    //void SpawnEnemy()
    //{
    //    bool isGameOver = false;

    //    while (isGameOver == false)
    //    {
    //        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    //        if (enemyCount < Random.Range(1,maxEnemy))
    //        {

    //            int index = Random.Range(0, spawnPoints.Count);
    //            Instantiate(enemyPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
