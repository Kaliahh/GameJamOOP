using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Editor fields
    [SerializeField] private int maxEnemies;
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnPointsParent;
    
    // Timer that keeps track of time until next possible spawn
    private float spawnTimer = 0.0f;
    // List of the available spawnpoints
    private List<Transform> spawnPoints = new List<Transform>();
    // List of spawned enemies
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
        // Check if there is room for more enemies
        if (enemies.Count < maxEnemies)
        {
            // Check if there is still time left until possible spawn
            if (spawnTimer > 0.0f)
            {
                // Remove passed time from timer, or set to 0 if it is done
                spawnTimer = (spawnTimer <= Time.deltaTime) ? 0f : spawnTimer - Time.deltaTime;
            } else {
                // Generate a random index of the spawn points
                int randomIndex = (int)Random.Range(0, spawnPoints.Count - 0.01f);
                // Set the spawn position to the random points position
                Vector3 spawnPos = spawnPoints[randomIndex].position;
                // Instantiate a new enemy
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, null);
                // Set the enemies player target to the player
                newEnemy.GetComponent<EnemyMovement>().Player = player;
                // Add it to the list of enemies
                enemies.Add(newEnemy);
                // Set the spawn timer to the spawn delay
                spawnTimer = spawnDelay;
            }
        }
    }
}
