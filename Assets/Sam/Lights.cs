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
        light_system.Update_SingleLights();
    }
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

        return null; // or throw an exception if you prefer
    }
}


// Class with the main product of the lights
public class LightSystem
{
    public List<SingleLight> light_list = new List<SingleLight>();
    private LightFactory lightFactory = new LightFactory();

    public void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                light_list.Add(lightFactory.CreateLight("WhiteLight", i, 5, j));
                light_list.Add(lightFactory.CreateLight("ColorLight", i, 5, -j, Color.red));
                light_list.Add(lightFactory.CreateLight("WhiteLight", -i, 5, j));
                light_list.Add(lightFactory.CreateLight("ColorLight", -i, 5, -j, Color.green));
            }
        }
/*        // four corners
        SingleLight corner1 = new WhiteLight(20, 5, 20);
        SingleLight corner2 = new WhiteLight(20, 5, -20);
        SingleLight corner3 = new WhiteLight(-20, 5, 20);
        SingleLight corner4 = new WhiteLight(-20, 5, -20);

        // four sectors
        SingleLight sector1 = new ColorLight(10, 5, 10, Color.red);
        SingleLight sector2 = new ColorLight(10, 5, -10, Color.red);
        SingleLight sector3 = new ColorLight(-10, 5, 10, Color.red);
        SingleLight sector4 = new ColorLight(-10, 5, -10, Color.red);*/
    }
    public void Update_SingleLights()
    {
        foreach (var light in light_list)
        {
            light.UpdateLight();
        }
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
        lightComp.range = 40;

        lightGameObject.transform.position = new Vector3(x, y, z);
    }

    virtual public void Init()
    {
        //public lightGameObject = new GameObject("SingleLight");
        //public lightComp = lightGameObject.AddComponent<Light>();
    }

    public virtual void UpdateLight()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            lightComp.enabled = !lightComp.enabled;
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

    public override void UpdateLight()
    {
        base.UpdateLight();

        // Toggle flashing mode with 'K' key
        if (Input.GetKeyUp(KeyCode.K))
        {
            isFlashing = !isFlashing;
        }

        if (isFlashing)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            lightComp.color = Color.Lerp(Color.red, Color.blue, t);
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

    public override void UpdateLight()
    {
        base.UpdateLight();

        if (Input.GetKeyUp(KeyCode.J))
        {
            isIntensify = !isIntensify;
        }

        if (isIntensify)
        {
            // Simple pulsing effect
            float intensity = Mathf.PingPong(Time.time, duration);
            lightComp.intensity = intensity + 0.5f; // Add base intensity to avoid complete darkness
        }
    }
}