using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    // Initalize vars
    Light myLight;
    float duration = 1.0f;
    Color red = Color.red;
    Color blue = Color.blue;
    Color green = Color.green;
    Color black = Color.black;
    Color yellow = Color.yellow;
    Color white = Color.white;


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