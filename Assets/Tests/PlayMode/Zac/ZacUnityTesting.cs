using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class ZacUnityTesting
{
    public class EnemySpawnerStressTest : MonoBehaviour
    {
        private EnemySpawner enemySpawner;
        private const string testSceneName = "ZacStressTest"; // Set this to the name of a test scene with NavMesh baked

        private int framesCount = 0;
        private float fpsTimer = 0f;
        private const float fpsCheckInterval = 1f; // Time interval to check FPS
        private const int fpsThreshold = 30; // FPS threshold for warnings

        [UnitySetUp]
        public IEnumerator Setup()
        {
            // Load the scene asynchronously
            yield return SceneManager.LoadSceneAsync(testSceneName, LoadSceneMode.Single);
            Debug.Log($"Loaded scene: {testSceneName}");

            // Ensure the scene has loaded and NavMesh is ready
            yield return new WaitForSeconds(1f); // Small delay to ensure NavMesh is loaded

               // Create spawner object and set up enemy spawner
            GameObject spawnerObject = new GameObject("SpawnerObject");
            enemySpawner = spawnerObject.AddComponent<EnemySpawner>();

            // Set up spawn points for testing
            enemySpawner.spawnPoints = new Transform[3];
            for (int i = 0; i < 3; i++)
            {
                GameObject spawnPointObject = new GameObject("SpawnPoint" + i);
                spawnPointObject.transform.position = new Vector3(i * -19f, 3f, 1); // Offset positions to avoid overlaps
                enemySpawner.spawnPoints[i] = spawnPointObject.transform;
                Debug.Log($"Spawn point {i} set at position: {spawnPointObject.transform.position}");
            }
        }

        [UnityTest]
        public IEnumerator StressTestEnemySpawning()
        {
            int totalEnemiesToSpawn = 2000; // Total enemies to spawn
            float delayBetweenSpawns = 0.0005f; // Delay between spawns (faster spawn for stress test)
            float testDuration = 20f; // Duration of the test in seconds
            float elapsedTime = 0f;

            while (elapsedTime < testDuration)
            {
                for (int i = 0; i < totalEnemiesToSpawn; i++)
                {
                    enemySpawner.SpawnEnemy("BasicEnemy");
                    Debug.Log($"Spawned BasicEnemy {i + 1}.");
                    yield return new WaitForSeconds(delayBetweenSpawns); // Stagger spawns to reduce overlapping

                    // Check frame rate every few spawns
                    framesCount++;
                    if (Time.time - fpsTimer >= fpsCheckInterval)
                    {
                        float fps = framesCount / fpsCheckInterval;
                        framesCount = 0;
                        fpsTimer = Time.time;

                        if (fps < fpsThreshold)
                        {
                            Debug.LogWarning($"FPS dropped below {fpsThreshold}: {fps}");
                        }
                    }
                }

                // Check current number of spawned enemies
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                Debug.Log($"Current enemies in scene: {enemies.Length}");

                // Optionally add a condition to break if the number of enemies exceeds a limit
                if (enemies.Length >= 2000) // Example limit, adjust as needed
                {
                    Debug.LogWarning("Maximum enemy count reached, stopping the test.");
                    break; // Exit if too many enemies are spawned
                }

                elapsedTime += totalEnemiesToSpawn * delayBetweenSpawns; // Update elapsed time
                yield return null; // Wait for the next frame
            }

            // Final check after test duration
            GameObject[] finalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            Assert.GreaterOrEqual(finalEnemies.Length, totalEnemiesToSpawn, $"Expected at least {totalEnemiesToSpawn} enemies to spawn during the test, but found {finalEnemies.Length}.");
            Debug.Log($"Total enemies spawned during the stress test: {finalEnemies.Length}");

            // Additional checks for NavMeshAgent setup
            foreach (GameObject enemy in finalEnemies)
            {
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                Assert.IsNotNull(agent, "Enemy does not have a NavMeshAgent component.");
                Assert.IsTrue(agent.isOnNavMesh, "Enemy agent is not on the NavMesh.");
                Debug.Log($"Enemy {enemy.name} is on NavMesh at position: {enemy.transform.position}");
            }

            Debug.Log("Stress test completed.");
        }
    }
}
