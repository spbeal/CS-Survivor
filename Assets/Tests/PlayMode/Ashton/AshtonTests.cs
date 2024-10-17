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
    public IEnumerator Test_MinExpPickup()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        var player = GameObject.FindWithTag("Player");
        var initialExp = ExpController.instance.currentExp;

        var expPickup = new GameObject("ExpPickup").AddComponent<ExpPickup>();
        expPickup.expValue = 0;

        // Simulate spawning experience at a certain position
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        ExpController.instance.SpawnExp(spawnPosition);

        expPickup.transform.position = player.transform.position;
        yield return new WaitForSeconds(0.1f);  // Simulate time for the pickup to be collected

        Assert.AreEqual(initialExp, ExpController.instance.currentExp, "Exp value should remain the same when picking up 0 EXP.");
    }


    [UnityTest]
    public IEnumerator Test_ExpSpawning()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        // Setup the exp prefab manually in the test if needed
        var expPrefab = new GameObject("ExpPickup").AddComponent<ExpPickup>();
        expPrefab.expValue = 10;  // Set a default value for testing

        // Ensure the pickup is assigned in the ExpController
        ExpController.instance.pickup = expPrefab;

        // Simulate spawning experience at a certain position
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        ExpController.instance.SpawnExp(spawnPosition);

        yield return null; // Wait a frame for the exp to spawn

        // Now we can check if the exp object has actually been instantiated
        var spawnedExp = GameObject.FindObjectOfType<ExpPickup>();
        Assert.IsNotNull(spawnedExp, "Experience did not spawn as expected.");

        yield return null;
    }


    [UnityTest]
    public IEnumerator Test_StressMultipleExpPickups()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        // Set up the exp prefab manually in the test
        var expPrefab = new GameObject("ExpPickup").AddComponent<ExpPickup>();
        expPrefab.expValue = 10;  // Assign default EXP value for the pickups

        // Ensure the pickup is assigned in the ExpController
        ExpController.instance.pickup = expPrefab;

        // Simulate spawning 1000 experience pickups
        int numPickups = 10000;
        for (int i = 0; i < numPickups; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));  // Random position for each pickup
            ExpController.instance.SpawnExp(randomPosition);
        }

        yield return null;  // Wait for the pickups to be instantiated

        // Find all the spawned experience pickups
        var spawnedPickups = GameObject.FindObjectsOfType<ExpPickup>();

        // Assert that the correct number of pickups were spawned
        Assert.AreEqual(numPickups, spawnedPickups.Length, "Not all experience pickups were spawned as expected.");

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
