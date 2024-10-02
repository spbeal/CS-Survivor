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