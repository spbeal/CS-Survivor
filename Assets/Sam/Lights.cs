using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Initalize vars
    private Light myLight;
    public float duration = 1.0f;
    private Color red = Color.red;
    private Color blue = Color.blue;
    private Color green = Color.green;
    private Color black = Color.black;
    private Color yellow = Color.yellow;
    private Color white = Color.white;


    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        // Light on off
        if (Input.GetKeyUp(KeyCode.L))
        {
            myLight.enabled = !myLight.enabled;
        }

        // Flashing light based on time
        float t = Mathf.PingPong(Time.time, duration) / duration;
        myLight.color = Color.Lerp(red, blue, t);
        //myLight.color = Color.Lerp(red, blue, t);
        //myLight.color = Color.Lerp(Lerp(green, Lerp(yellow, white)), Lerp(red, Lerp(blue, black)), t);
        //myLight.color = Color.Lerp(green, white, t);
        //myLight.color = Color.Lerp(black, yellow, t);


    }
}

// Class with all the light functionality. 

public class LightSystem
{
    // Start is called before the first frame update
/*    LightSystem()
    {
        for (int i = 0; i < 10; i++)
        {
            WhiteLight a = new WhiteLight();
        }
        for (int i = 0; i < 10; i++)
        {
            ColorLight a = new ColorLight();
        }
    }*/

}

public class ColorLight : LightSystem
{
    ColorLight()
    {
        // Make a game object
        GameObject lightGameObject = new GameObject("ColorLight");

        // Add the light component
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set color and position
        lightComp.color = Color.red;

        // Set the position (or any transform property)
        lightGameObject.transform.position = new Vector3(0,0,0);
    }
    ColorLight(int x, int y, int z, Color color)
    {
        // Make a game object
        GameObject lightGameObject = new GameObject("ColorLight");

        // Add the light component
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set color and position
        lightComp.color = color;

        // Set the position (or any transform property)
        lightGameObject.transform.position = new Vector3(x, y, z);
    }
}

public class WhiteLight : LightSystem
{
    WhiteLight()
    {
        // Make a game object
        GameObject lightGameObject = new GameObject("WhiteLight");

        // Add the light component
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set color and position
        lightComp.color = Color.white;

        // Set the position (or any transform property)
        lightGameObject.transform.position = new Vector3(0,0,0);
    }
    WhiteLight(int x, int y, int z)
    {
        // Make a game object
        GameObject lightGameObject = new GameObject("WhiteLight");

        // Add the light component
        Light lightComp = lightGameObject.AddComponent<Light>();

        // Set color and position
        lightComp.color = Color.white;

        // Set the position (or any transform property)
        lightGameObject.transform.position = new Vector3(x, y, z);
    }
}