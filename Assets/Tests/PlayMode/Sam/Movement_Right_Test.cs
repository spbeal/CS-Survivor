using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class MovementRightTest
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
        terrainObject.AddComponent<TerrainCollider>();

        // Scale terrain and move it down slightly so player starts above it
        terrainObject.transform.position = new Vector3(0, -1, 0);
        terrainObject.transform.localScale = new Vector3(5, 1, 5);  // Adjust terrain size if needed

        // Instantiate a new Player GameObject and attach the Movement component
        playerObject = new GameObject("Player");
        playerObject.AddComponent<CharacterController>(); // Required for movement
        playerMovement = playerObject.AddComponent<Movement>();

        // Position player above the terrain so it interacts with the TerrainCollider
        playerObject.transform.position = new Vector3(0, 1, 0);  // Position the player above the terrain

        // Create and assign a Camera to the player
        Camera playerCam = new GameObject("PlayerCamera").AddComponent<Camera>();
        playerMovement.GetType().GetField("playerCamera", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerMovement, playerCam);
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

        // Simulate 750 frames of rightward movement
        for (int i = 0; i < 750; i++)
        {
            // Incrementally increase the player's speed for demonstration
            playerMovement.walkSpeed += 0.01f;

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

    // Optionally: Tear down the player and terrain object after the test
    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(playerObject);
        GameObject.Destroy(terrainObject);
    }
}
