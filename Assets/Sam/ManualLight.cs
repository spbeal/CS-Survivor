using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualLight : MonoBehaviour
{
    private Light myLight;
    public float duration = 1.0f;
    private bool state = true;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            myLight.enabled = !myLight.enabled;
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            state = !state;
        }
        if (state)
        {
            myLight.color = Color.white;
        }
        else
        {
            // Flashing light based on time
            float t = Mathf.PingPong(Time.time, duration) / duration;
            myLight.color = Color.Lerp(Color.red, Color.blue, t);
        }
    }
}
