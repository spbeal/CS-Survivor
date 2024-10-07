using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MovementTest
{
    private GameObject playerObject;
    private Movement playerMovement;
    private GameObject terrainObject;

    // Use the [SetUp] attribute to create the player and terrain before each test.
    [SetUp]
    public void Setup()
    {
        // Create Terrain GameObject
        terrainObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        terrainObject.name = "Terrain";
        terrainObject.AddComponent<Terrain>();
        //terrainObject.AddComponent<TerrainCollider>();

        Terrain terrainMain = GameObject.FindObjectOfType<Terrain>();
        //terrainObject = Terrain.CreateTerrainGameObject(terrainMain.terrainData);
        TerrainCollider terrainCollider = terrainObject.AddComponent<TerrainCollider>();
        terrainCollider.terrainData = terrainMain.terrainData;

        // Scale terrain and move it down slightly so player starts above it
        terrainObject.transform.position = new Vector3(0, -1, 0);
        terrainObject.transform.localScale = new Vector3(5, 1, 5);  // Adjust terrain size if needed

        // Instantiate a new Player GameObject and attach the Movement component
        playerObject = new GameObject("Player");
        playerObject.AddComponent<CharacterController>(); // Required for movement
        playerMovement = playerObject.AddComponent<Movement>();

        // Position player above the terrain so it interacts with the TerrainCollider
        playerObject.transform.position = new Vector3(0, 0, 0);  // Position the player above the terrain

        // Create and assign a Camera to the player
        Camera playerCam = new GameObject("PlayerCamera").AddComponent<Camera>();
        playerMovement.GetType().GetField("playerCamera", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerMovement, playerCam);
    }

    [UnityTest]
    public IEnumerator movement_test_left()
    {
        // Wait for the scene to load (if applicable, not needed here)
        yield return new WaitForEndOfFrame();

        // Get the initial position of the player
        float initialPosX = playerObject.transform.position.x;

        // Set custom moveDirection to simulate leftward movement
        Vector3 leftMove = Vector3.left * playerMovement.walkSpeed;

        for (int i = 0; i < 10; i++)
        {
            // Incrementally increase the player's speed for demonstration
            playerMovement.walkSpeed += 0.1f;

            // Move player to the left with the updated speed
            leftMove = Vector3.left * playerMovement.walkSpeed;
            playerMovement.GetComponent<CharacterController>().Move(leftMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = playerObject.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        Assert.Less(finalPosX, initialPosX, "The player did not move left as expected.");
    }

    [UnityTest]
    public IEnumerator movement_test_right()
    {
        // Wait for the scene to load (if applicable, not needed here)
        yield return new WaitForEndOfFrame();

        // Get the initial position of the player
        float initialPosX = playerObject.transform.position.x;

        // Set custom moveDirection to simulate rightward movement
        Vector3 rightMove = Vector3.right * playerMovement.walkSpeed;

        for (int i = 0; i < 10; i++)
        {
            // Incrementally increase the player's speed for demonstration
            playerMovement.walkSpeed += 0.1f;

            // Move player to the right with the updated speed
            rightMove = Vector3.right * playerMovement.walkSpeed;
            playerMovement.GetComponent<CharacterController>().Move(rightMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = playerObject.transform.position.x;

        // Assert that the player has moved right (the x position should increase)
        Assert.Greater(finalPosX, initialPosX, "The player did not move right as expected.");
    }

    [UnityTest]
    public IEnumerator movement_test_stress()
    {
        // Wait for the scene to load (if applicable, not needed here)
        yield return new WaitForEndOfFrame();

        // Get the initial position of the player
        float initialPosX = playerObject.transform.position.x;

        // Set custom moveDirection to simulate leftward movement
        Vector3 leftMove = Vector3.left * playerMovement.walkSpeed;
        Vector3 rightMove = Vector3.right * playerMovement.walkSpeed;

        // Simulate 1000 frames
        for (int i = 0; i < 1000; i++)
        {
            // Incrementally increase the player's speed for demonstration
            playerMovement.walkSpeed += 0.01f;

            // Move player with updated speed
            if (i % 2 == 0)
            {
                leftMove = Vector3.left * playerMovement.walkSpeed;
                playerMovement.GetComponent<CharacterController>().Move(leftMove * Time.deltaTime);
            }
            else
            {
                rightMove = Vector3.right * playerMovement.walkSpeed;
                playerMovement.GetComponent<CharacterController>().Move(rightMove * Time.deltaTime);
            }

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosX = playerObject.transform.position.x;

        // Assert that the player has moved left (the x position should decrease)
        Assert.Less(finalPosX, initialPosX, "The player did not move as expected.");
    }

    // Optionally: Tear down the player and terrain object after the test
    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(playerObject);
        GameObject.Destroy(terrainObject);
    }
}
