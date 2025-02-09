using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


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

    public static float GetCurrentFPS()
    {
        return 1.0f / Time.unscaledDeltaTime;
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // Identify and delete any temporary scenes created during testing
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });

        foreach (string guid in scenePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (path.Contains("InitTestScene")) // Adjust to match naming convention
            {
                AssetDatabase.DeleteAsset(path);
                Debug.Log($"Deleted temporary scene: {path}");
            }
        }

        // Optionally unload loaded test scenes
        if (sceneLoaded)
        {
            SceneManager.UnloadSceneAsync("Main/MinimumViableProduct");
            Debug.Log("Unloaded test scene.");
        }
    }

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

    [UnityTest]
    public IEnumerator CreateLightsStress()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load

        LightSystem a = new LightSystem();
        int i = 1;
        bool pass = false;
        for (i = 0; i < 100; i++)
        {
            //a.Init();
            a.TestCreate(100);
            if (GetCurrentFPS() < 60.0f)
            {
                pass = true;
                break;
            }
        }
        List<SingleLight> lights = a.GetAllLights();

        if (pass)
        {
            Assert.Pass("Passed. Number of lights created: " + i * 500 + " Actual number: " + lights.Count + " After " + i + " frames");
        }
        Assert.Fail("Failed. Current FPS: " + GetCurrentFPS() + " Number of lights created: " + (i * 50) + " Actual number: " + lights.Count + " After " + i + " frames");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator FlickerEffectsAndLights()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load

        LightSystem a = new LightSystem();
        a.Init();

        bool LightOn = false;
        bool EffectOn = false;

        for (int i = 0; i < 10; i++)
        {
            a.Init();
        }
        List<SingleLight> lights = a.GetAllLights();

        for (int i = 0; i < 100; i++)
        {
            a.Update_SingleLights(false, false);
            foreach (SingleLight lightobj in lights)
            {
                if (lightobj.GetEffect())
                {
                    EffectOn = true;
                    break;
                }
                else
                {
                    EffectOn = false;
                }

                Light tmp = a.GetLightComp(lightobj);
                if (tmp.enabled)
                {
                    LightOn = true;
                    break;
                }
                else
                {
                    LightOn = false;
                }
            }

            if (LightOn == true || EffectOn == true)
            {
                Assert.Fail("Failed. Number of lights: " + lights.Count + "After " + i + " frames");
            }

            a.Update_SingleLights(true, true);
            foreach (SingleLight lightobj in lights)
            {
                if (lightobj.GetEffect())
                {
                    EffectOn = true;
                }
                else
                {
                    EffectOn = false;
                    break;
                }

                Light tmp = a.GetLightComp(lightobj);
                if (tmp.enabled)
                {
                    LightOn = true;
                }
                else
                {
                    LightOn = false;
                    break;
                }
            }

            if (LightOn == false || EffectOn == false)
            {
                Assert.Fail("Failed. Number of lights: " + lights.Count + "After " + i + " frames");
            }
        }
        Assert.Pass("Everything worked properly with a large number of lights turning both the lights and effects on and off");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator FlickerEffects()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load

        LightSystem a = new LightSystem();
        a.Init();

        bool LightOn = false;
        bool EffectOn = false;

        for (int i = 0; i < 10; i++)
        {
            a.Init();
        }
        List<SingleLight> lights = a.GetAllLights();

        for (int i = 0; i < 100; i++)
        {
            a.Update_SingleLights(true, false);
            foreach (SingleLight lightobj in lights)
            {
                if (lightobj.GetEffect())
                {
                    EffectOn = true;
                    break;
                }
                else
                {
                    EffectOn = false;
                }
            }

            if (EffectOn == true)
            {
                Assert.Fail("Failed. Number of lights: " + lights.Count + "After " + i + " frames");
            }

            a.Update_SingleLights(true, true);
            foreach (SingleLight lightobj in lights)
            {
                if (lightobj.GetEffect())
                {
                    EffectOn = true;
                }
                else
                {
                    EffectOn = false;
                    break;
                }
            }

            if (EffectOn == false)
            {
                Assert.Fail("Failed. Number of lights: " + lights.Count + "After " + i + " frames");
            }
        }
        Assert.Pass("Everything worked properly with a large number of lights turning the effects on and off");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator FlickerLights()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        // Wait for the scene to load

        LightSystem a = new LightSystem();
        a.Init();

        bool LightOn = false;
        bool EffectOn = false;

        for (int i = 0; i < 10; i++)
        {
            a.Init();
        }
        List<SingleLight> lights = a.GetAllLights();
        for (int i = 0; i < 100; i++)
        {
            a.Update_SingleLights(false, false);
            foreach (SingleLight lightobj in lights)
            {
                Light tmp = a.GetLightComp(lightobj);
                if (tmp.enabled)
                {
                    LightOn = true;
                    break;
                }
                else
                {
                    LightOn = false;
                }
            }

            if (LightOn == true)
            {
                Assert.Fail("Failed. Number of lights: " + lights.Count + "After " + i + " frames");
            }

            a.Update_SingleLights(true, false);
            foreach (SingleLight lightobj in lights)
            {

                Light tmp = a.GetLightComp(lightobj);
                if (tmp.enabled)
                {
                    LightOn = true;
                }
                else
                {
                    LightOn = false;
                    break;
                }
            }

            if (LightOn == false)
            {
                Assert.Fail("Failed. Number of lights: " + lights.Count + "After " + i + " frames");
            }
        }
        Assert.Pass("Everything worked properly with a large number of lights turning them on and off");

        yield return null; // Wait for the next frame
    }

}
