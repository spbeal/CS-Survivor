using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    Light myLight;
    float duration = 1.0f;
    Color red = Color.red;
    Color blue = Color.blue;
    Color green = Color.green;
    Color black = Color.black;
    Color yellow = Color.yellow;
    Color white = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.L))
        {
            myLight.enabled = !myLight.enabled;
        }

        float t = Mathf.PingPong(Time.time, duration) / duration;
        //myLight.color = Color.Lerp(red, blue, t);
        //myLight.color = Color.Lerp(Lerp(green, Lerp(yellow, white)), Lerp(red, Lerp(blue, black)), t);
        myLight.color = Color.Lerp(red, blue, t);
/*        myLight.color = Color.Lerp(green, white, t);
        myLight.color = Color.Lerp(black, yellow, t);*/
    }
}