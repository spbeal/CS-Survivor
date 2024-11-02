using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class SamLightTests
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

/*        
var Light = GameObject.Find("LightManager").GetComponent<Lights>();
var LightFactorys = GameObject.Find("LightManager").GetComponent<LightFactory>();
var LightSystems = GameObject.Find("LightManager").GetComponent<LightSystem>();
var LightSingle = GameObject.Find("LightManager").GetComponent<SingleLight>();
var LightColor = GameObject.Find("LightManager").GetComponent<ColorLight>();
var LightWhite = GameObject.Find("LightManager").GetComponent<WhiteLight>();
*/

    // Create an object of each class and test if they exist
    // Create lights and turn them on and off
    // Create lights and stress test for flickering (Color, White, and Single Light
    // Create lightsystems until it breaks the game
    
    // TESTS --------------------------------------
    [UnityTest]
    public IEnumerator light_on()
    {
        var LightFactorys = GameObject.Find("LightManager").GetComponent<LightFactory>();

        if (LightFactorys.CreateLight("SingleLight", 0, 0, 0) != null)
        {
            Assert.Pass("One SingleLight was created");
        }
        else
        {
            Assert.Fail("No SingleLight was created");
        }
        yield return null; // Wait for the next frame
    }
}
