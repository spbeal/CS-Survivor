using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


//[TestFixture]
//[UnityPlatform(exclude = new[] { RuntimePlatform.WindowsEditor })] // Exclude Play Mode if running in Edit Mode only
public class SamEditLightTests
{
    // Create an object of each class and test if they exist
    // Create lights and turn them on and off
    // Create lights and stress test for flickering (Color, White, and Single Light
    // Create lightsystems until it breaks the game

    // TESTS --------------------------------------
    /*    
        LightFactory a = new LightFactory();
        LightSystem a = new LightSystem();

        SingleLight a = new SingleLight(0, 0, 0);

        SingleLight a = new ColorLight(0, 0, 0, Color.red);

        SingleLight a = new WhiteLight(0, 0, 0);
    */


    [UnityTest]
    public IEnumerator LightOffEffectOff()
    {
        LightSystem a = new LightSystem();
        a.Init();
        a.Update_SingleLights(false, false);
        List<SingleLight> lights = a.GetAllLights();

        bool LightOn = false;
        bool EffectOn = false;
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

        if (LightOn == false && EffectOn == false)
        {
            Assert.Pass("Effect is off and light is off");
        }
        else
        {
            Assert.Fail("Error");
        }

        yield return null; // Wait for the next frame
    }
    [UnityTest]
    public IEnumerator LightOnEffectOff()
    {
        LightSystem a = new LightSystem();
        a.Init();
        a.Update_SingleLights(true, false);
        List<SingleLight> lights = a.GetAllLights();

        bool LightOn = true;
        bool EffectOn = false;
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
            }
            else
            {
                LightOn = false;
                break;
            }
        }

        if(LightOn && EffectOn == false)
        {
            Assert.Pass("Effect is off and light is on");
        }
        else
        {
            Assert.Fail("Error");
        }

        yield return null; // Wait for the next frame
    }
    [UnityTest]
    public IEnumerator LightOnEffectOn()
    {
        LightSystem a = new LightSystem();
        a.Init();
        a.Update_SingleLights(true, true);
        List<SingleLight> lights = a.GetAllLights();

        bool LightOn = true;
        bool EffectOn = true;
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

        if (LightOn && EffectOn)
        {
            Assert.Pass("Effect is on and light is on");
        }
        else
        {
            Assert.Fail("Error");
        }

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
    }
}
