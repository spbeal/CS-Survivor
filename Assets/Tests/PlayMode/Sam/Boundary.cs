using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

//Switch between scenes is a great stress test

public class SamTests
{
    private GameObject playerObject;
    private Movement playerMovement;
    private GameObject terrainObject;

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
    public IEnumerator movement_test_left()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        float initialPosX = Player.transform.position.x;

        Vector3 leftMove = Vector3.left * _Movement.walkSpeed;

        for (int i = 0; i < 5000; i++)
        {

            // Move player to the left with the updated speed
            leftMove = Vector3.left * _Movement.walkSpeed;
            Player.Move(leftMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        float finalPosX = Player.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        if (finalPosX < -18)
            Assert.Fail("The player did not move left as expected.");
        Assert.Pass("The player did move left as expected. " + finalPosX);
    }

    [UnityTest]
    public IEnumerator movement_test_right()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        float initialPosX = Player.transform.position.x;

        Vector3 rightMove = Vector3.right * _Movement.walkSpeed;

        for (int i = 0; i < 5000; i++)
        {

            // Move player to the left with the updated speed
            rightMove = Vector3.right * _Movement.walkSpeed;
            Player.Move(rightMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = Player.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        if (finalPosX > 18)
            Assert.Fail("The player did not move left as expected.");
        Assert.Pass("The player did move left as expected. Position: " + finalPosX);
    }

    [UnityTest]
    public IEnumerator movement_test_stress()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        // Get the initial position of the player
        float initialPosX = Player.transform.position.x;
        float finalPosX = 0;

        // Set custom moveDirection to simulate leftward movement
        Vector3 leftMove = Vector3.left * _Movement.walkSpeed;
        Vector3 rightMove = Vector3.right * _Movement.walkSpeed;

        // Simulate 10000 frames
        for (int i = 0; i < 10000; i++)
        {
            _Movement.walkSpeed += 2f;

            // Move player with updated speed to the left
            leftMove = Vector3.left * _Movement.walkSpeed;
            Player.Move(leftMove * Time.deltaTime);


            finalPosX = Player.transform.position.x;
            if (finalPosX < -20) // boundary
                Assert.Pass("The player moved from " + initialPosX + " to" + finalPosX + " at speed " + _Movement.walkSpeed);

            yield return null; // Wait for the next frame
        }

        Assert.Fail("The stress test failed to break the game");
    }
}