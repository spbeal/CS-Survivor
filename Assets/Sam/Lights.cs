using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    Light myLight;
    float duration = 1.0f;
    Color red = Color.red;
    Color blue = Color.blue;


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
            myLight.color = Color.Lerp(red, blue, t);
        
    }
}