using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array to hold spawn points
    private EnemyFactory enemyFactory; // Reference to the enemy factory

    private void Start()
    {
        // Initialize the enemy factory
        enemyFactory = new EnemyFactory();
    }

    private void Update()
    {
        // Check for input to spawn enemies
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to spawn an enemy
        {
            SpawnEnemy("BasicEnemy"); // Change this to "AdvancedEnemy" to spawn advanced enemies
        }
    }

    private void SpawnEnemy(string enemyType)
    {
        // Ensure there are spawn points
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Select a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Create the enemy using the factory
        IEnemy enemy = enemyFactory.CreateEnemy(enemyType);

        // Spawn the enemy at the selected location
        if (enemy != null)
        {
            enemy.Spawn(spawnPoint.position);
        }
        else
        {
            Debug.LogError($"Enemy type {enemyType} could not be created!");
        }
    }
}

