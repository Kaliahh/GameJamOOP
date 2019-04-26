using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnPointsParent;

    private float spawnTimer = 0.0f;
    private List<Transform> spawnPoints = new List<Transform>();
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        // Initialize list of spawnpoints
        for (int i = 0; i < spawnPointsParent.transform.childCount; ++i)
        {
            spawnPoints.Add(spawnPointsParent.transform.GetChild(i).transform);
        }
    }

    void Update()
    {
        if (enemies.Count < maxEnemies)
        {
            if (spawnTimer > 0.0f)
            {
                spawnTimer = (spawnTimer <= Time.deltaTime) ? 0f : spawnTimer - Time.deltaTime;
            } else {
                int randomIndex = (int)Random.Range(0, spawnPoints.Count - 0.01f);
                Vector3 spawnPos = spawnPoints[randomIndex].position;
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, null);
                newEnemy.GetComponent<EnemyMovement>().Player = player;
                enemies.Add(newEnemy);
                spawnTimer = spawnDelay;
            }
        }
    }
}
