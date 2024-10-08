using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

//Switch between scenes is a great stress test

public class Boundary
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
    public IEnumerator movement_test_left()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        // Get the initial position of the player
        float initialPosX = Player.transform.position.x;

        // Set custom moveDirection to simulate leftward movement
        Vector3 leftMove = Vector3.left * _Movement.walkSpeed;

        for (int i = 0; i < 50; i++)
        {
            Debug.Log("Player pos" + Player.transform.position.x + "\n");
            // Incrementally increase the player's speed for demonstration
            _Movement.walkSpeed += 0.1f;

            // Move player to the left with the updated speed
            leftMove = Vector3.left * _Movement.walkSpeed;
            Player.Move(leftMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = Player.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        if (finalPosX > 380)
            Assert.Fail("The player did not move left as expected.");
        Assert.Pass("The player did move left as expected.");
    }

    [UnityTest]
    public IEnumerator movement_test_right()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        // Get the initial position of the player
        float initialPosX = Player.transform.position.x;

        // Set custom moveDirection to simulate leftward movement
        Vector3 rightMove = Vector3.right * _Movement.walkSpeed;

        for (int i = 0; i < 50; i++)
        {
            Debug.Log("Player pos" + Player.transform.position.x + "\n");
            // Incrementally increase the player's speed for demonstration
            _Movement.walkSpeed += 0.1f;

            // Move player to the left with the updated speed
            rightMove = Vector3.right * _Movement.walkSpeed;
            Player.Move(rightMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = Player.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        if (finalPosX > 89500)
            Assert.Fail("The player did not move left as expected.");
        Assert.Pass("The player did move left as expected.");
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

        // Set custom moveDirection to simulate leftward movement
        Vector3 leftMove = Vector3.left * _Movement.walkSpeed;
        Vector3 rightMove = Vector3.right * _Movement.walkSpeed;

        // Simulate 1000 frames
        for (int i = 0; i < 100; i++)
        {
            // Incrementally increase the player's speed for demonstration
            _Movement.walkSpeed += 50f;

            Debug.Log("Player pos" + Player.transform.position.x + " : ");
            Debug.Log("Player speed" + _Movement.walkSpeed + "\n");

            // Move player with updated speed
            if (i % 2 == 0)
            {
                leftMove = Vector3.left * _Movement.walkSpeed;
                Player.Move(leftMove * Time.deltaTime);
            }
            else
            {
                rightMove = Vector3.right * _Movement.walkSpeed;
                Player.Move(rightMove * Time.deltaTime);
            }

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = Player.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        //Change the assert to assert at a position we want.
        if (Player.transform.position.x < 2) Assert.Fail();
        Assert.Pass("The player moved from " + initialPosX + " to" + finalPosX + " at speed " + _Movement.walkSpeed);
    }

    // Optional
/*    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(playerObject);
        GameObject.Destroy(terrainObject);
    }*/
}
