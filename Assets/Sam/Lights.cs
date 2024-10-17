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
        a.Draw();
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
    // Start is called before the first frame update
    public LightSystem()
    {

    }
    public void Init()
    {
        // four corners
        WhiteLight corner1 = new WhiteLight(16, 5, 16);
        WhiteLight corner2 = new WhiteLight(16, 5, -16);
        WhiteLight corner3 = new WhiteLight(-16, 5, 16);
        WhiteLight corner4 = new WhiteLight(-16, 5, -16);

        // four sectors
        ColorLight sector1 = new ColorLight(10, 4, 10, Color.red);
        ColorLight sector2 = new ColorLight(10, 4, -10, Color.red);
        ColorLight sector3 = new ColorLight(-10, 4, 10, Color.red);
        ColorLight sector4 = new ColorLight(-10, 4, -10, Color.red);
    }
    public void Draw()
    {

    }
}

public class ColorLight : LightSystem
{
    public ColorLight()
    {
        GameObject lightGameObject = new GameObject("ColorLight");
        Light lightComp = lightGameObject.AddComponent<Light>();

        lightComp.color = Color.red ;

        lightGameObject.transform.position = new Vector3(0, 0, 0);
    }
    public ColorLight(int x, int y, int z, Color color)
    {
        GameObject lightGameObject = new GameObject("ColorLight");
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set parameters
        lightComp.color = color;
        lightComp.type = LightType.Point;
        lightComp.range = 40;

        lightGameObject.transform.position = new Vector3(x, y, z);
    }
}

public class WhiteLight : LightSystem
{
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