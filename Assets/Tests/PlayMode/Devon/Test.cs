using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

//Switch between scenes is a great stress test

public class Test
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
		
		
		
    }

    [UnityTest]
    public IEnumerator movement_test_stress()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();
        var _PlayerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        float walkSpeed = _PlayerStats.GetSpeed();

        // Get the initial position of the player
		float initialPosX = Player.transform.position.x;
        float finalPosX = 0;
        float initialPosY = Player.transform.position.y;
        float finalPosY = 0;

        // Set custom moveDirection to simulate leftward movement
        //Vector3 leftMove = Vector3.left * _Movement.walkSpeed;
        //Vector3 rightMove = Vector3.right * _Movement.walkSpeed;

        // Simulate 10000 frames
        for (int i = 0; i < 10000; i++)
        {
            _Movement.moveDirection.y += -100f;
			//Player.Move(_Movement.moveDirection * Time.deltaTime);


            // Move player with updated speed to the left
            //leftMove = Vector3.left * _Movement.walkSpeed;
            //Player.Move(leftMove * Time.deltaTime);


            finalPosY = Player.transform.position.y;
            if (finalPosY < -20 || finalPosX < -20) // boundary
                Assert.Pass("The player moved from " + initialPosY + " to" + finalPosY + " at speed " + walkSpeed);

            yield return null; // Wait for the next frame
        }

        Assert.Fail("The stress test failed to break the game");
    }
}
