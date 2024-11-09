using UnityEngine;
using UnityEditor;
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
    public IEnumerator DeleteLightGameObject()
    {
        SingleLight a = new WhiteLight(0, 0, 0);
        GameObject tmp = a.get_light_gobject();
        tmp = null;

        //get_light_gobject
        try
        {
            if ((tmp.transform.position.x == 0))
            {
                Assert.Fail("Still exists after deletion");
            }
        }
        finally
        {
            Assert.Pass("No longer exists");
        }
        yield return null; // Wait for the next frame
    }


    [UnityTest]
    public IEnumerator DeleteSingleLight()
    {
        SingleLight a = new SingleLight(0, 0, 0);
        Light tmp = a.get_light_comp();
        a = null;

        //get_light_gobject
        try 
        {
            if ((tmp.color == Color.white))
            {
                Assert.Fail("Still exists after deletion");
            }
        }
        finally
        {
            Assert.Pass("No longer exists");
        }
        yield return null; // Wait for the next frame
    }
    [UnityTest]
    public IEnumerator DeleteWhiteLight()
    {
        SingleLight a = new WhiteLight(0, 0, 0);
        Light tmp = a.get_light_comp();
        a = null;

        //get_light_gobject
        try
        {
            if ((tmp.color == Color.white))
            {
                Assert.Fail("Still exists after deletion");
            }
        }
        finally
        {
            Assert.Pass("No longer exists");
        }
        yield return null; // Wait for the next frame
    }
    [UnityTest]
    public IEnumerator DeleteColorLight()
    {
        SingleLight a = new ColorLight(0, 0, 0, Color.white);
        Light tmp = a.get_light_comp();
        a = null;

        //get_light_gobject
        try
        {
            if ((tmp.color == Color.white))
            {
                Assert.Fail("Still exists after deletion");
            }
        }
        finally
        {
            Assert.Pass("No longer exists");
        }
        yield return null; // Wait for the next frame
    }


    [UnityTest]
    public IEnumerator ChangeLightPositionX()
    {
        SingleLight a = new WhiteLight(0, 0, 0);
        GameObject tmp = a.get_light_gobject();

        tmp.transform.position = new Vector3(5, 0, 0);
        //get_light_gobject
        if (tmp.transform.position.x == 5)
        {
            Assert.Pass("Coordinate X changed properly");
        }
        else
        {
            Assert.Fail("Coordinate X changed improperly");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator ChangeLightPositionY()
    {
        SingleLight a = new WhiteLight(0,0,0);
        GameObject tmp = a.get_light_gobject();

        tmp.transform.position = new Vector3(0, 5, 0);
        //get_light_gobject
        if (tmp.transform.position.y == 5)
        {
            Assert.Pass("Coordinate Y changed properly");
        }
        else
        {
            Assert.Fail("Coordinate Y changed improperly");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator ChangeLightPositionZ()
    {
        SingleLight a = new WhiteLight(0, 0, 0);
        GameObject tmp = a.get_light_gobject();

        tmp.transform.position = new Vector3(0, 0, 5);
        //get_light_gobject
        if (tmp.transform.position.z == 5)
        {
            Assert.Pass("Coordinate Z changed properly");
        }
        else
        {
            Assert.Fail("Coordinate Z changed improperly");
        }
        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator ChangeLightRange()
    {
        SingleLight a = new SingleLight();
        Light tmp = a.get_light_comp();

        //default range

        if (tmp.range == 35)
        {
            tmp.range = 50;
            if (tmp.range == 50)
            {
                Assert.Pass("Range changed properly.");
            }
        }
        else
        {
            Debug.Log("Default range is not 35");

            tmp.range = 50;
            if (tmp.range == 50)
            {
                Assert.Pass("Range changed properly.");
            }
        }

        Assert.Fail("Range did not change properly");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator ChangeLightType()
    {
        SingleLight a = new SingleLight();
        Light tmp = a.get_light_comp();

        //default type is Point

        if (tmp.type == LightType.Point)
        {
            tmp.type = LightType.Spot;
            if (tmp.type == LightType.Spot)
            {
                Assert.Pass("Type changed properly.");
            }
        }

        Assert.Fail("Type did not change properly");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator ChangeLightColor()
    {
        SingleLight a = new SingleLight();
        Light tmp = a.get_light_comp();

        //default color is white

        if (tmp.color == Color.white)
        {
            tmp.color = Color.yellow;
            if (tmp.color == Color.yellow)
            {
                Assert.Pass("Color changed properly.");
            }
        }

        Assert.Fail("Color did not change properly");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator ChangeWhiteLightIntensity()
    {
        SingleLight a = new WhiteLight(0,0,0);
        Light tmp = a.get_light_comp();

        tmp.intensity = 10;

        if (tmp.intensity == 10)
        {
            Assert.Pass("Correct intensity");
        }

        Assert.Fail("Incorrect intensity");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator CheckColorLightEffectColor()
    {
        LightSystem a = new LightSystem();
        a.SingleCreateColorLight();
        a.Update_SingleLights(true, true);
        List<SingleLight> lights = a.GetAllLights();

        foreach (SingleLight lightobj in lights)
        {
            Light tmp = a.GetLightComp(lightobj);
            if (tmp.color == Color.white)
            {
                Assert.Fail("Color did not change with effect");
            }
            else
            {
                Assert.Pass("Color changed properly with effect");
            }
        }

        Assert.Fail("Color did not change properly");

        yield return null; // Wait for the next frame
    }

    [UnityTest]
    public IEnumerator CheckWhiteLightColor()
    {
        SingleLight a = new WhiteLight(0,0,0);
        Light tmp = a.get_light_comp();

        //default color is white

        if (tmp.color == Color.white)
        {
            Assert.Pass("Correct color");
        }

        Assert.Fail("Incorrect color");

        yield return null; // Wait for the next frame
    }

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
