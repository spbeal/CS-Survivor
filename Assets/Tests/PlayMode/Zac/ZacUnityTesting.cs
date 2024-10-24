using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZacUnityTesting
{
    private const float expectedSightRange = 10f; // Adjust as needed
    private bool sceneLoaded;

    [SetUp]
    public void Setup()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Zac/ZacTesting", LoadSceneMode.Single);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneLoaded = true;
    }

    [UnityTest]
    public IEnumerator TestEnemySightRange_Boundary()
    {
        // Instantiate or access the EnemyFactory
        EnemyFactory enemyFactory = new EnemyFactory();

        // Access the spawn point (adjust if necessary)
        Transform spawnPoint = GameObject.Find("SpawnPoint1").transform;

        // Create and spawn a basic enemy using the factory
        IEnemy enemy = enemyFactory.CreateEnemy("BasicEnemy");
        enemy.Spawn(spawnPoint.position);

        // Wait a frame to ensure the enemy is instantiated
        yield return null;

        // Now, access the spawned enemy
        GameObject spawnedEnemy = GameObject.FindWithTag("BasicEnemy");

        // Ensure the spawned enemy exists
        Assert.IsNotNull(spawnedEnemy, "BasicEnemy was not found after spawning.");

        // Access the EnemyBehavior component to test sight range
        EnemyBehavior enemyBehavior = spawnedEnemy.GetComponent<EnemyBehavior>();
        Assert.IsNotNull(enemyBehavior, "EnemyBehavior component not found!");

        // Set sight range to a very low value and test
        enemyBehavior.sightRange = 1f; // assuming sightRange is a public variable
        Assert.AreEqual(1f, enemyBehavior.sightRange);

        // Set sight range to a very high value and test
        enemyBehavior.sightRange = 100f;
        Assert.AreEqual(100f, enemyBehavior.sightRange);
    }

    [UnityTest]
    public IEnumerator TestEnemySpeed()
    {
        // Instantiate or access the EnemyFactory
        EnemyFactory enemyFactory = new EnemyFactory();

        // Access the spawn point
        Transform spawnPoint = GameObject.Find("SpawnPoint1").transform;

        // Create a basic enemy using the factory and spawn it
        IEnemy enemy = enemyFactory.CreateEnemy("BasicEnemy");
        enemy.Spawn(spawnPoint.position); // Spawn enemy

        // Wait a frame to ensure the enemy is instantiated
        yield return null;

        // Find the last instantiated enemy by tag
        GameObject spawnedEnemy = GameObject.FindGameObjectWithTag("BasicEnemy");

        Assert.IsNotNull(spawnedEnemy, "BasicEnemy was not found after spawning.");

        // Ensure the spawned enemy has a NavMeshAgent component
        NavMeshAgent navAgent = spawnedEnemy.GetComponent<NavMeshAgent>();
        Assert.IsNotNull(navAgent, "NavMeshAgent component not found!");

        // Set the enemy's speed
        float expectedSpeed = 5f;   // Set the speed you want to test
        navAgent.speed = expectedSpeed;

        // Set a target destination for the enemy to move towards
        Vector3 targetPosition = new Vector3(10f, 0f, 10f);
        navAgent.SetDestination(targetPosition);

        // Wait for a short amount of time to let the enemy start moving
        yield return new WaitForSeconds(0.5f);

        // Check that the enemy's current speed is close to the expected speed
        float currentSpeed = navAgent.velocity.magnitude;
        Assert.LessOrEqual(currentSpeed, expectedSpeed, "Enemy is moving too fast!");
        Assert.Greater(currentSpeed, 0f, "Enemy has not started moving!");

        // Let the enemy continue moving and check that it maintains the expected speed
        yield return new WaitForSeconds(1.5f);

        currentSpeed = navAgent.velocity.magnitude;
        Assert.AreEqual(expectedSpeed, currentSpeed, 0.1f, "Enemy did not reach the correct speed!");

        Debug.Log("Enemy reached correct speed: " + currentSpeed);
    }
}

public class StressTestEnemySpawning
{
    private EnemyFactory enemyFactory;
    private Transform spawnPoint;
    private const int enemyCount = 100; // Number of enemies to spawn
    private const float spawnInterval = 0.1f; // Time between spawns
    private List<GameObject> spawnedEnemies;

    [SetUp]
    public void Setup()
    {
        enemyFactory = new EnemyFactory();
        spawnedEnemies = new List<GameObject>();
        GameObject spawnPointObject = GameObject.Find("SpawnPoint1");
        Assert.IsNotNull(spawnPointObject, "SpawnPoint1 not found!");
        spawnPoint = spawnPointObject.transform;
    }
/* Compiler error prevents me from doing anything (line 139) so I'm commenting this out for now (you can fix this and uncomment it!)
    [UnityTest]
    public IEnumerator TestEnemySpawningStress()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            IEnemy enemy = enemyFactory.CreateEnemy("BasicEnemy");
            enemy.Spawn(spawnPoint.position);

            // Add spawned enemy to the list
            spawnedEnemies.Add(enemy.GameObject);

            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }

        // Wait a bit longer to ensure all enemies are instantiated
        yield return new WaitForSeconds(2f);

        // Check that the number of spawned enemies is as expected
        Assert.AreEqual(enemyCount, spawnedEnemies.Count, "Not all enemies were spawned!");

        Debug.Log($"{spawnedEnemies.Count} enemies spawned successfully.");
    }
*/
}
