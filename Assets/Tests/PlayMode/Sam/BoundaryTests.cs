using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

//Switch between scenes is a great stress test

public class SamTests
{
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

    [UnityTest]
    public IEnumerator boundary_left()
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
    public IEnumerator boundary_right()
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
    public IEnumerator boundary_stress()
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