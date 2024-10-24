using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array to hold spawn points
    private EnemyFactory enemyFactory; // Reference to the enemy factory

    private int currentRound = 1; // Start at round 1
    private int maxRounds = 5; // Limit to 5 rounds
    public int enemiesPerRound = 3; // Number of enemies to spawn per round *NOTE* This should probably be private but is public for the sake of testing*

    private void Start()
    {
        // Initialize the enemy factory
        enemyFactory = new EnemyFactory();
    }

    private void Update()
    {
        // Check for input to start a new round (Enter key)
        if (Input.GetKeyDown(KeyCode.Return) && currentRound <= maxRounds)
        {
            StartRound();
            currentRound++; // Move to the next round
        }
    }

    private void StartRound()
    {
        Debug.Log($"Starting round {currentRound}");

        for (int i = 0; i < enemiesPerRound; i++)
        {
            string enemyType = GetWeightedEnemyType(currentRound); // Get the enemy type based on the current round
            SpawnEnemy(enemyType);
        }
    }

    private string GetWeightedEnemyType(int round)
    {
        // Adjust the weighting based on the round number
        float randomValue = Random.value;

        switch (round)
        {
            case 1:
                return randomValue < 0.8f ? "BasicEnemy" : "AdvancedEnemy"; // 80% Basic, 20% Advanced
            case 2:
                return randomValue < 0.7f ? "BasicEnemy" : "AdvancedEnemy"; // 70% Basic, 30% Advanced
            case 3:
                return randomValue < 0.6f ? "BasicEnemy" : "AdvancedEnemy"; // 60% Basic, 40% Advanced
            case 4:
                return randomValue < 0.5f ? "BasicEnemy" : "AdvancedEnemy"; // 50% Basic, 50% Advanced
            case 5:
                return randomValue < 0.4f ? "BasicEnemy" : "AdvancedEnemy"; // 40% Basic, 60% Advanced
            default:
                return "BasicEnemy"; // Default case, shouldn't happen with 5 rounds
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
















//Old system below   |
//                   |
//                   V


/*using System.Collections;
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

*/