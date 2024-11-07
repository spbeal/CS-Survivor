using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Initalize vars
    private LightSystem light_system;

    void Start()
    {
        //myLight = GetComponent<Light>();
        light_system = new LightSystem();
        light_system.Init();
    }

    void Update()
    {
        //light_system.Update_SingleLights();
    }

/*    SingleLight TestLightFactory(string lightType, int x, int y, int z, Color? color = null)
    {
        LightFactory a;
        a.CreateLight(lightType, x, y, z, color);
    }*/
/*    void TestLightSystem()
    {
        LightSystem a;
    }*/
}


public class LightFactory
{
    public SingleLight CreateLight(string lightType, int x, int y, int z, Color? color = null)
    {
        if (lightType == "WhiteLight")
        {
            return new WhiteLight(x, y, z);
        }
        else if (lightType == "ColorLight" && color != null)
        {
            return new ColorLight(x, y, z, color.Value);
        }
        else if (lightType == "SingleLight")
        {
            return new SingleLight(x, y, z);
        }

        return null; // or throw an exception if you prefer
    }
}


// Class with the main product of the lights
public class LightSystem
{
    private List<SingleLight> light_list = new List<SingleLight>();
    private LightFactory lightFactory = new LightFactory();

    public void Init()
    {
/*        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                light_list.Add(lightFactory.CreateLight("WhiteLight", i, 5, j));
                light_list.Add(lightFactory.CreateLight("ColorLight", i, 5, -j, Color.yellow));
                light_list.Add(lightFactory.CreateLight("WhiteLight", -i, 5, j));
                light_list.Add(lightFactory.CreateLight("ColorLight", -i, 5, -j, Color.yellow));
            }
        }*/
        for (int i = 0; i < 10; i++)
        {
            //Middle
            //light_list.Add(lightFactory.CreateLight("WhiteLight", (-i * 15)+30, 4, 0));
            light_list.Add(lightFactory.CreateLight("ColorLight", (-i * 15), 7, 0, Color.white));
            //Sides
            light_list.Add(lightFactory.CreateLight("WhiteLight",  (-i * 15), 0, 22));
            light_list.Add(lightFactory.CreateLight("ColorLight",  (-i * 15), 7, -22, Color.white));
            light_list.Add(lightFactory.CreateLight("WhiteLight",  (-i * 15), 0, -22));
            light_list.Add(lightFactory.CreateLight("ColorLight",  (-i * 15), 7, 22, Color.white));
        }
    }

    public void Update_SingleLights(bool inputOn, bool inputEffect)
    {
        foreach (var light in light_list)
        {
            light.UpdateLight(inputOn, inputEffect);
        }
    }

    // Additional testing
    public List<SingleLight> GetAllLights()
    {
        return light_list;
    }

    public int LightCount()
    {
        return light_list.Count;
    }
}

public class SingleLight
{
    public GameObject lightGameObject;
    public Light lightComp;
    public float duration = 1.0f;

    public SingleLight()
    {
        lightGameObject = new GameObject("SingleLight");
        lightComp = lightGameObject.AddComponent<Light>();
        //lightComp.enabled = false;
    }
    public SingleLight(int x, int y, int z)
    {
        lightGameObject = new GameObject("SingleLight");
        lightComp = lightGameObject.AddComponent<Light>();
        //lightComp.enabled = false;

        lightComp.color = Color.yellow;
        lightComp.type = LightType.Point;
        lightComp.range = 35;

        lightGameObject.transform.position = new Vector3(x, y, z);
    }

    virtual public void Init()
    {
        //public lightGameObject = new GameObject("SingleLight");
        //public lightComp = lightGameObject.AddComponent<Light>();
    }

    public virtual void UpdateLight(bool inputOn, bool inputEffect)
    {
        if (inputOn)
        {
            lightComp.enabled = true;
        }
        else
        {
            lightComp.enabled = false;
        }
    }
    //virtual public void 
    // change color, change type, change location, something random and movable.

    //virtual lightComp.type = LightType.Point;
    // Virtual void apply() and other functions to change type, range, color and such by default,
    // but it gets overriden and changed in our subclasses. Instead of ColorLight having a constructor, 
    // Have it use the virtual function and override it. 

}

public class ColorLight : SingleLight
{
    private bool isFlashing = false;
    public ColorLight(int x, int y, int z, Color color) : base(x, y, z)
    {
        lightComp.color = color;
    }

    public override void UpdateLight(bool inputOn, bool inputEffect)
    {
        base.UpdateLight(inputOn, inputEffect);

        // Toggle flashing mode with 'K' key
        if (inputEffect)
        {
            isFlashing = true;
        }
        else
        {
            isFlashing = false;
        }

        if (isFlashing)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            lightComp.color = Color.Lerp(Color.red, Color.blue, t);
        }
        else
        {
            lightComp.color = Color.white;
        }
    }
}

public class WhiteLight : SingleLight
{
    private bool isIntensify = false;
    public WhiteLight(int x, int y, int z) : base(x, y, z)
    {
        lightComp.color = Color.white;
    }

    public override void UpdateLight(bool inputOn, bool inputEffect)
    {
        base.UpdateLight(inputOn, inputEffect);

        if (inputEffect)
        {
            isIntensify = true;
        }
        else
        {
            isIntensify = false;
        }

        if (isIntensify)
        {
            // Simple pulsing effect
            float intensity = Mathf.PingPong(Time.time, duration);
            lightComp.intensity = intensity + 0.5f; // Add base intensity to avoid complete darkness
        }
        else
        {
            //lightComp.intensity = 0.5f;
        }
    }
}
/* 
    IEnumerator WaitAndPrint()
    {
        Debug.Log("Starting coroutine, waiting 3 seconds...");
        
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);
        
        Debug.Log("3 seconds passed!");
        
        // Add other actions here after the wait
    }
*/