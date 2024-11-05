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

    // Downwards 
    [UnityTest]
    public IEnumerator boundary_floor()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();


        Vector3 downMove = Vector3.down * _Movement.walkSpeed;

        for (int i = 0; i < 2000; i++)
        {

            // Move player to the left with the updated speed
            downMove = Vector3.down * _Movement.walkSpeed;
            Player.Move(downMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        float finalPosY = Player.transform.position.y;

        if (finalPosY < -0.5)
            Assert.Fail("The player did not move back as expected.");
        Assert.Pass("The player did move back as expected. " + finalPosY);
    }

    // Backwards = vector3.right
    [UnityTest]
    public IEnumerator boundary_back()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();


        Vector3 backMove = Vector3.right * _Movement.walkSpeed;

        for (int i = 0; i < 2000; i++)
        {

            // Move player to the left with the updated speed
            backMove = Vector3.right * _Movement.walkSpeed;
            Player.Move(backMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        float finalPosX = Player.transform.position.x;

        if (finalPosX > 28.5)
            Assert.Fail("The player did not move back as expected.");
        Assert.Pass("The player did move back as expected. " + finalPosX);
    }
    // Forward = vector3.left
    [UnityTest]
    public IEnumerator boundary_front()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();


        Vector3 forwardMove = Vector3.left * _Movement.walkSpeed;

        // Push to left wall to check front boundary
        Vector3 leftMove = Vector3.forward * _Movement.walkSpeed;

        for (int i = 0; i < 500; i++)
        {

            // Move player to the left with the updated speed
            leftMove = Vector3.forward * _Movement.walkSpeed;
            Player.Move(leftMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        for (int i = 0; i < 5000; i++)
        {

            // Move player to the left with the updated speed
            forwardMove = Vector3.left * _Movement.walkSpeed;
            Player.Move(forwardMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        float finalPosX = Player.transform.position.x;

        if (finalPosX < -263)
            Assert.Fail("The player did not move forward as expected." + finalPosX);
        Assert.Pass("The player did move forward as expected. " + finalPosX);
    }


    [UnityTest]
    public IEnumerator boundary_left()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();


        Vector3 leftMove = Vector3.forward * _Movement.walkSpeed;

        for (int i = 0; i < 2000; i++)
        {

            // Move player to the left with the updated speed
            leftMove = Vector3.forward * _Movement.walkSpeed;
            Player.Move(leftMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        float finalPosZ = Player.transform.position.z;

        // Assert that the player has moved left (the x position should decrease)
        if (finalPosZ < -79.5)
            Assert.Fail("The player did not move left as expected.");
        Assert.Pass("The player did move left as expected. " + finalPosZ);
    }

    [UnityTest]
    public IEnumerator boundary_right()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        Vector3 rightMove = Vector3.back * _Movement.walkSpeed;

        for (int i = 0; i < 2000; i++)
        {

            // Move player to the left with the updated speed
            rightMove = Vector3.back * _Movement.walkSpeed;
            Player.Move(rightMove * Time.deltaTime);

            yield return null; // Wait for the next frame
        }

        // Get the final position of the player
        float finalPosZ = Player.transform.position.z;

        // Assert that the player has moved left (the x position should decrease)
        if (finalPosZ > 79.5)
            Assert.Fail("The player did not move left as expected.");
        Assert.Pass("The player did move left as expected. Position: " + finalPosZ);
    }

    /*
    Vector3.right (1, 0, 0): points along the positive x-axis, usually to the right.
    Vector3.left (-1, 0, 0): points along the negative x-axis, usually to the left.
    Vector3.forward (0, 0, 1): points along the positive z-axis, usually forward in 3D space.
    Vector3.back (0, 0, -1): points along the negative z-axis, usually backward.
    Vector3.up (0, 1, 0): points along the positive y-axis, usually upward.
    Vector3.down (0, -1, 0): points along the negative y-axis, usually downward.
     */
    [UnityTest]
    public IEnumerator boundary_stress()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load (if applicable, not needed here)
        //yield return new WaitForEndOfFrame();

        var Player = GameObject.Find("Player").GetComponent<CharacterController>();
        var _Movement = GameObject.Find("Player").GetComponent<Movement>();

        // Get the initial position of the player
        float initialPosZ = Player.transform.position.z;
        float finalPosZ = 0;

        // Set custom moveDirection to simulate leftward movement
        Vector3 rightMove = Vector3.forward * _Movement.walkSpeed;
        Vector3 currentPos = Player.transform.position;

        // Simulate 10000 frames
        for (int i = 0; i < 10000; i++)
        {
            currentPos.z = 0.0f;
            Player.transform.position = currentPos;
            yield return new WaitForSeconds(0.05f);

            _Movement.walkSpeed += 50000f;

            //for (int j = 0; j < 1000; j++)
            //{
                // Move player with updated speed to the left
                rightMove = Vector3.forward * _Movement.walkSpeed;
                Player.Move(rightMove * Time.deltaTime);

                finalPosZ = Player.transform.position.z;
                if (finalPosZ > 80) // boundary
                    Assert.Pass("The player moved from " + initialPosZ + " to" + finalPosZ + " at speed " + _Movement.walkSpeed);
            //}

            yield return null; // Wait for the next frame
        }

        Assert.Fail("The stress test failed to break the game");
    }
}