using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class SamPlayLightTests
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
     *        Following does not work, create a typical instant since they aren't derived from monobehavior
     *        MyClass myInstance = new MyClass();
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
    /*    LightFactory a = new LightFactory();
        LightSystem a = new LightSystem();

        SingleLight a = new SingleLight();
        SingleLight a = new SingleLight(0, 0, 0);

        SingleLight a = new ColorLight();
        SingleLight a = new ColorLight(0, 0, 0, Color.red);

        SingleLight a = new WhiteLight();
        SingleLight a = new WhiteLight(0, 0, 0);
    */

/*    [UnityTest]
    public IEnumerator OnOffSingleLight()
    {
        SingleLight a = new SingleLight();
        //if (a.enabled)
        Assert.Pass("One SingleLight was created");
        Assert.Fail("No SingleLight was created");
        yield return null; // Wait for the next frame
    }
    [UnityTest]
    public IEnumerator OnOffWhiteLight()
    {
        Assert.Pass("One SingleLight was created");

        yield return null; // Wait for the next frame
    }
    [UnityTest]
    public IEnumerator OnOffColorLight()
    {
        Assert.Pass("One SingleLight was created");

        yield return null; // Wait for the next frame
    }

    // Create Lights
    [UnityTest]
    public IEnumerator CreateSingleLight()
    {
        SingleLight a = new SingleLight();
        if (a != null)
        {
            Assert.Pass("One SingleLight was created");
        }
        else
        {
            Assert.Fail("No SingleLight was created");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator CreateWhiteLight()
    {
        SingleLight a = new WhiteLight(0,0,0);
        if (a != null)
        {
            Assert.Pass("One WhiteLight was created");
        }
        else
        {
            Assert.Fail("No WhiteLight was created");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator CreateColorLight()
    {
        SingleLight a = new ColorLight(0, 0, 0, Color.red);
        if (a != null)
        {
            Assert.Pass("One ColorLight was created");
        }
        else
        {
            Assert.Fail("No ColorLight was created");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator FactoryCreateSingleLight()
    {
        LightFactory a = new LightFactory();
        if (a.CreateLight("SingleLight", 0, 0, 0) != null)
        {
            Assert.Pass("One SingleLight was created");
        }
        else
        {
            Assert.Fail("No SingleLight was created");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator FactoryCreateWhiteLight()
    {
        LightFactory a = new LightFactory();
        if (a.CreateLight("WhiteLight", 0, 0, 0) != null)
        {
            Assert.Pass("One WhiteLight was created");
        }
        else
        {
            Assert.Fail("No WhiteLight was created");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator FactoryCreateColorLight()
    {
        LightFactory a = new LightFactory();
        if (a.CreateLight("ColorLight", 0, 0, 0, Color.red) != null)
        {
            Assert.Pass("One ColorLight was created");
        }
        else
        {
            Assert.Fail("No ColorLight was created");
        }
        yield return null; // Wait for the next frame
    }*/
}
