using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections.Generic;

public class zacMoreTesting
{
    [Test]
    public void EnemySpawner_InitializesCorrectly()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        Assert.IsNotNull(spawner, "EnemySpawner did not initialize correctly.");
    }

    [Test]
    public void EnemySpawner_SpawnPointsAssigned()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[3];
        for (int i = 0; i < 3; i++)
        {
            spawner.spawnPoints[i] = new GameObject($"SpawnPoint{i}").transform;
        }
        Assert.AreEqual(3, spawner.spawnPoints.Length, "Spawn points were not assigned correctly.");
    }

    [Test]
    public void EnemyPrefabs_ExistInResources()
    {
        var basicEnemyPrefab = Resources.Load<GameObject>("Prefabs/BasicEnemy");
        Assert.IsNotNull(basicEnemyPrefab, "BasicEnemy prefab not found in Resources.");

        var advancedEnemyPrefab = Resources.Load<GameObject>("Prefabs/AdvancedEnemy");
        Assert.IsNotNull(advancedEnemyPrefab, "AdvancedEnemy prefab not found in Resources.");
    }

    [Test]
    public void EnemyFactory_ReturnsCorrectEnemyTypes()
    {
        var factory = new EnemyFactory();

        var basicEnemy = factory.CreateEnemy("BasicEnemy");
        Assert.IsInstanceOf<BasicEnemy>(basicEnemy, "Factory did not return BasicEnemy.");

        var advancedEnemy = factory.CreateEnemy("AdvancedEnemy");
        Assert.IsInstanceOf<AdvancedEnemy>(advancedEnemy, "Factory did not return AdvancedEnemy.");
    }

    [Test]
    public void EnemyFactory_ReturnsNullForUnknownType()
    {
        var factory = new EnemyFactory();
        var unknownEnemy = factory.CreateEnemy("UnknownEnemy");

        Assert.IsNull(unknownEnemy, "Factory should return null for unknown enemy type.");
    }

    [Test]
    public void EnemySpawner_SpawnPointsDistributedProperly()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[5];
        for (int i = 0; i < 5; i++)
        {
            spawner.spawnPoints[i] = new GameObject($"SpawnPoint{i}").transform;
            spawner.spawnPoints[i].position = new Vector3(i * 10, 0, i * 5);
        }

        foreach (var point in spawner.spawnPoints)
        {
            Assert.IsNotNull(point, "Spawn point is null.");
        }
    }

    [Test]
    public void EnemySpawner_SpawnsMultipleEnemies()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[1] { new GameObject("SpawnPoint").transform };

        for (int i = 0; i < 5; i++)
        {
            spawner.SpawnEnemy("BasicEnemy");
        }

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Assert.AreEqual(5, enemies.Length, "EnemySpawner did not spawn the expected number of enemies.");
    }

    [Test]
    public void EnemyBehavior_InitializesNavMeshAgent()
    {
        var enemy = new GameObject().AddComponent<EnemyBehavior>();
        var navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        Assert.IsNotNull(navMeshAgent, "NavMeshAgent was not initialized correctly.");
    }

    [Test]
    public void EnemySpawner_NullSpawnPoints()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = null;

        Assert.DoesNotThrow(() => spawner.SpawnEnemy("BasicEnemy"), "EnemySpawner should handle null spawn points gracefully.");
    }

    [Test]
    public void EnemySpawner_ThrowsErrorWhenNoSpawnPoints()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        Assert.Throws<System.Exception>(() => spawner.SpawnEnemy("BasicEnemy"), "EnemySpawner did not throw an error when no spawn points were set.");
    }

    [Test]
    public void EnemySpawner_UsesAllSpawnPoints()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[3] {
        new GameObject("SpawnPoint1").transform,
        new GameObject("SpawnPoint2").transform,
        new GameObject("SpawnPoint3").transform
    };

        for (int i = 0; i < 3; i++)
        {
            spawner.SpawnEnemy("BasicEnemy");
        }

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Assert.AreEqual(3, enemies.Length, "EnemySpawner did not use all available spawn points.");
    }

    [Test]
    public void EnemySpawner_HandlesNullSpawnPoints()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[1]; // Array with null entry

        Assert.Throws<System.NullReferenceException>(() => spawner.SpawnEnemy("BasicEnemy"), "EnemySpawner did not handle null spawn points gracefully.");
    }

    [Test]
    public void MultipleInstancesOfEnemySpawner_CanCoexist()
    {
        var spawner1 = new GameObject().AddComponent<EnemySpawner>();
        var spawner2 = new GameObject().AddComponent<EnemySpawner>();

        Assert.IsNotNull(spawner1);
        Assert.IsNotNull(spawner2);
        Assert.AreNotSame(spawner1, spawner2, "Multiple instances of EnemySpawner should coexist without issues.");
    }

    [Test]
    public void EnemySpawner_LogsErrorWhenPrefabMissing()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[1] { new GameObject("SpawnPoint").transform };

        LogAssert.Expect(LogType.Error, "Enemy prefab not found or invalid.");
        spawner.SpawnEnemy("InvalidEnemy");
    }

    [Test]
    public void EnemySpawner_PerformanceIsAcceptable()
    {
        var spawner = new GameObject().AddComponent<EnemySpawner>();
        spawner.spawnPoints = new Transform[10];
        for (int i = 0; i < 10; i++)
        {
            spawner.spawnPoints[i] = new GameObject($"SpawnPoint{i}").transform;
        }

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 100; i++)
        {
            spawner.SpawnEnemy("BasicEnemy");
        }
        stopwatch.Stop();

        Assert.Less(stopwatch.ElapsedMilliseconds, 500, "EnemySpawner is taking too long to spawn enemies.");
    }

}