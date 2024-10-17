using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Spawn(Vector3 location);
}

public class BasicEnemy : IEnemy
{
    private GameObject prefab; // Reference to the enemy prefab

    public BasicEnemy()
    {
        prefab = Resources.Load<GameObject>("Prefabs/BasicEnemy"); // Load the prefab from the Resources folder
    }

    public void Spawn(Vector3 location)
    {
        if (prefab != null)
        {
            GameObject enemyInstance = GameObject.Instantiate(prefab, location, Quaternion.identity);
            Debug.Log("Spawning BasicEnemy at " + location);
        }
        else
        {
            Debug.LogError("BasicEnemy prefab not found!");
        }
    }
}

public class AdvancedEnemy : IEnemy
{
    private GameObject prefab; // Reference to the enemy prefab

    public AdvancedEnemy()
    {
        prefab = Resources.Load<GameObject>("Prefabs/AdvancedEnemy"); // Load the prefab from the Resources folder
    }

    public void Spawn(Vector3 location)
    {
        if (prefab != null)
        {
            GameObject enemyInstance = GameObject.Instantiate(prefab, location, Quaternion.identity);
            Debug.Log("Spawning AdvancedEnemy at " + location);
        }
        else
        {
            Debug.LogError("AdvancedEnemy prefab not found!");
        }
    }
}

/*
  public class Boss : IEnemy
{
    private GameObject prefab; // Reference to the enemy prefab

    public Boss()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Boss"); // Load the prefab from the Resources folder
    }

    public void Spawn(Vector3 location)
    {
        if (prefab != null)
        {
            GameObject enemyInstance = GameObject.Instantiate(prefab, location, Quaternion.identity);
            Debug.Log("Spawning Boss at " + location);
        }
        else
        {
            Debug.LogError("Boss prefab not found!");
        }
    }
}
*/

public class EnemyFactory
{
    public IEnemy CreateEnemy(string enemyType)
    {
        switch (enemyType)
        {
            case "BasicEnemy":
                return new BasicEnemy();
            case "AdvancedEnemy":
                return new AdvancedEnemy();
            default:
                return null;
        }
    }
}

