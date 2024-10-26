using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Initalize vars
    private LightSystem a;

    void Start()
    {
        //myLight = GetComponent<Light>();
        a = new LightSystem();
        a.Init();
    }

    void Update()
    {
/*      // Light on off
        if (Input.GetKeyUp(KeyCode.L))
        {
            myLight.enabled = !myLight.enabled;
        }

        // Flashing light based on time
        float t = Mathf.PingPong(Time.time, duration) / duration;
        myLight.color = Color.Lerp(Color.red, Color.blue, t);

*/
    }
}

// Class with all the light functionality. 

public class LightSystem
{
    public void Init()
    {
        // four corners
        SingleLight corner1 = new WhiteLight(20, 5, 20);
        SingleLight corner2 = new WhiteLight(20, 5, -20);
        SingleLight corner3 = new WhiteLight(-20, 5, 20);
        SingleLight corner4 = new WhiteLight(-20, 5, -20);

        // four sectors
        SingleLight sector1 = new ColorLight(10, 5, 10, Color.red);
        SingleLight sector2 = new ColorLight(10, 5, -10, Color.red);
        SingleLight sector3 = new ColorLight(-10, 5, 10, Color.red);
        SingleLight sector4 = new ColorLight(-10, 5, -10, Color.red);
    }
}

public class SingleLight
{
    public GameObject lightGameObject;
    public Light lightComp;

    virtual public void Init()
    {

    }
    //virtual lightComp.type = LightType.Point;
    // Virtual void apply() and other functions to change type, range, color and such by default,
    // but it gets overriden and changed in our subclasses. Instead of ColorLight having a constructor, 
    // Have it use the virtual function and override it. 
}

public class ColorLight : SingleLight
{
    override public void Init()
    {

    }
    public ColorLight()
    {
        void Init()
        {

        }
        lightGameObject = new GameObject("ColorLight");
        lightComp = lightGameObject.AddComponent<Light>();

        lightComp.color = Color.red ;

        lightGameObject.transform.position = new Vector3(0, 0, 0);
    }
    void Init(int x, int y, int z, Color color)
    {

    }
    public ColorLight(int x, int y, int z, Color color)
    {
        lightGameObject = new GameObject("ColorLight");
        lightComp = lightGameObject.AddComponent<Light>();

        // Set parameters
        lightComp.color = color;
        lightComp.type = LightType.Point;
        lightComp.range = 40;

        lightGameObject.transform.position = new Vector3(x, y, z);
    }
}

public class WhiteLight : SingleLight
{
    void Init()
    {

    }
    void Init(int x, int y, int z)
    {

    }
    public WhiteLight()
    {
        GameObject lightGameObject = new GameObject("WhiteLight");
        Light lightComp = lightGameObject.AddComponent<Light>();

        lightComp.color = Color.white;

        lightGameObject.transform.position = new Vector3(0,0,0);
    }
    public WhiteLight(int x, int y, int z)
    {
        GameObject lightGameObject = new GameObject("WhiteLight");
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set parameters
        lightComp.color = Color.white;
        lightComp.type = LightType.Point;
        lightComp.range = 40;

        lightGameObject.transform.position = new Vector3(x, y, z);
    }
}
