using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

//Switch between scenes is a great stress test

public class AshtonTests
{
    private GameObject playerObject;
    private Movement playerMovement;
    private GameObject terrainObject;

/*    private var Player;
    private var _Movement;*/

    private bool sceneLoaded;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Main/MinimumViableProduct", LoadSceneMode.Single);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneLoaded = true;
    }

    // Use this to create your own scene with player, terrain, and camera.
    [SetUp]
    public void Setup()
    {
        /* // Create Terrain GameObject
         terrainObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
         terrainObject.name = "Terrain";
         terrainObject.AddComponent<Terrain>();

         Terrain terrainMain = GameObject.FindObjectOfType<Terrain>();
         TerrainCollider terrainCollider = terrainObject.AddComponent<TerrainCollider>();
         terrainCollider.terrainData = terrainMain.terrainData;

         // Scale terrain and move it down slightly so player starts above it
         terrainObject.transform.position = new Vector3(0, -1, 0);
         terrainObject.transform.localScale = new Vector3(5, 1, 5);  // Adjust terrain size if needed*/

        // Instantiate a new Player GameObject and attach the Movement component
        //playerObject = new GameObject("Player");
        //playerObject.AddComponent<CharacterController>(); // Required for movement
        //playerMovement = playerObject.AddComponent<Movement>();

        // Position player above the terrain so it interacts with the TerrainCollider
        //playerObject.transform.position = new Vector3(0, 0, 0);  // Position the player above the terrain

        // Create and assign a Camera to the player
        /*        Camera playerCam = new GameObject("PlayerCamera").AddComponent<Camera>();
                playerMovement.GetType().GetField("playerCamera", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(playerMovement, playerCam);*/
    }

    [UnityTest]
    public IEnumerator Test_MinGoldPickup()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        var player = GameObject.FindWithTag("Player");
        var initialGold = GoldController.instance.GetCurrentGold();

        var GoldPickup = new GameObject("GoldPickup").AddComponent<GoldPickup>();
//        GoldPickup._GoldValue = 0;

        // Simulate spawning Golderience at a certain position
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        GoldController.instance.SpawnGold(spawnPosition);

        GoldPickup.transform.position = player.transform.position;
        yield return new WaitForSeconds(0.1f);  // Simulate time for the pickup to be collected

        Assert.AreEqual(initialGold, GoldController.instance.GetCurrentGold(), "Gold value should remain the same when picking up 0 Gold.");
    }


    [UnityTest]
    public IEnumerator Test_GoldSpawning()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        // Setup the Gold prefab manually in the test if needed
        var GoldPrefab = new GameObject("GoldPickup").AddComponent<GoldPickup>();
//        GoldPrefab._GoldValue = 10;  // Set a default value for testing

        // Ensure the pickup is assigned in the GoldController
        GoldController.instance.pickup = GoldPrefab;

        // Simulate spawning Golderience at a certain position
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        GoldController.instance.SpawnGold(spawnPosition);

        yield return null; // Wait a frame for the Gold to spawn

        // Now we can check if the Gold object has actually been instantiated
        var spawnedGold = GameObject.FindObjectOfType<GoldPickup>();
        Assert.IsNotNull(spawnedGold, "Golderience did not spawn as Goldected.");

        yield return null;
    }


    [UnityTest]
    public IEnumerator Test_StressMultipleGoldPickups()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        // Set up the Gold prefab manually in the test
        var GoldPrefab = new GameObject("GoldPickup").AddComponent<GoldPickup>();
//        GoldPrefab._GoldValue = 10;  // Assign default Gold value for the pickups

        // Ensure the pickup is assigned in the GoldController
        GoldController.instance.pickup = GoldPrefab;

        // Simulate spawning 1000 Golderience pickups
        int numPickups = 10000;
        for (int i = 0; i < numPickups; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));  // Random position for each pickup
            GoldController.instance.SpawnGold(randomPosition);
        }

        yield return null;  // Wait for the pickups to be instantiated

        // Find all the spawned Golderience pickups
        var spawnedPickups = GameObject.FindObjectsOfType<GoldPickup>();

        // Assert that the correct number of pickups were spawned
        Assert.AreEqual(numPickups, spawnedPickups.Length, "Not all Golderience pickups were spawned as Goldected.");

        yield return new WaitForSeconds(5f); // Optional wait time for any further interactions or testing
    }


    // Optional
/*    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(playerObject);
        GameObject.Destroy(terrainObject);
    }*/
}
