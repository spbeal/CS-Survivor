using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class InitialTest
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
        var menuManager = gobject.GetComponent<PauseMenuFacade>();
        Assert.IsNotNull(menuManager, "MenuManager script not found");

        bool gamePausedUnpausedCorrectly = true;
        float delayTime = 1f;

        while(gamePausedUnpausedCorrectly){
            menuManager.ResumeGame();
            yield return null;
            if(Time.timeScale != 1){ // could be 1f? I'll be darned if I can tell
                gamePausedUnpausedCorrectly = false;
                break;
            }

            yield return new WaitForSeconds(delayTime);

            menuManager.PauseGame();
            yield return null;
            if(Time.timeScale != 0){
                gamePausedUnpausedCorrectly = false;
                break;
            }

            yield return new WaitForSeconds(delayTime);
            Debug.Log(delayTime);

            delayTime = delayTime / 2;
        }
            Assert.Pass("passed at time %f", delayTime); // idk it worked ???


    }


    [UnityTest]
    public IEnumerator ManyMenTest()
    {
        // Wait until the scene is loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        // Find the MenuManager in the scene
        var MManager = GameObject.Find("MenuManager");

        // Ensure the MenuManager exists in the scene
        Assert.IsNotNull(MManager, "MenuManager not found in the scene");

        // Get the MenuManager component from the GameObject
        var menuManager = MManager.GetComponent<PauseMenuFacade>();
        Assert.IsNotNull(menuManager, "MenuManager script not found");

        var Enemy = GameObject.Find("Enemy1");

        GameObject[] ManyMen = new GameObject[100];
        for(int i = 0; i < 100; i++)
        {
            ManyMen[i] = GameObject.Instantiate(Enemy); // praying this works rn
        }

        menuManager.PauseGame();
        Assert.AreEqual(Time.timeScale, 0);

        menuManager.ResumeGame();
        Assert.AreEqual(Time.timeScale, 1);

    }
};
