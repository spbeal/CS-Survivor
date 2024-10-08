using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class InitialTests
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
    public IEnumerator PauseFunctionTest()
    {
        // Wait until the scene is loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        // Find the MenuManager in the scene
        var gobject = GameObject.Find("MenuManager");

        // Ensure the MenuManager exists in the scene
        Assert.IsNotNull(gobject, "MenuManager not found in the scene");

        // Get the MenuManager component from the GameObject
        var menuManager = gobject.GetComponent<MenuManager>();
        Assert.IsNotNull(menuManager, "MenuManager script not found");

        // Verify that the game starts paused
        Assert.AreEqual(0f, Time.timeScale, "Game did not start paused");

        // Resume the game by simulating the PlayButton click
        menuManager.ResumeGame();  // or simulate the button click via the PlayButton

        // Check if the game is resumed (time scale should be 1)
        yield return null;  // wait a frame for the timeScale change
        Assert.AreEqual(1f, Time.timeScale, "Game did not resume properly");

        // Simulate pausing the game via the Escape key
        menuManager.PauseGame();  // alternatively simulate Input.GetKeyDown("escape")

        // Ensure the game is paused (time scale should be 0)
        yield return null;  // wait a frame for the timeScale change
        Assert.AreEqual(0f, Time.timeScale, "Game did not pause properly");

        // Simulate resuming the game again via the Escape key
        menuManager.ResumeGame();  // alternatively simulate Input.GetKeyDown("escape")

        // Ensure the game is resumed again (time scale should be 1)
        yield return null;  // wait a frame for the timeScale change
        Assert.AreEqual(1f, Time.timeScale, "Game did not resume properly after second pause");
    }
}
