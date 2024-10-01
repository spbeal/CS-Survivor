using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyScript : MonoBehaviour
{

    private float time = 10f;
    private float timeLeft;

    void Start() {
        timeLeft = time;
    }

    public void Update() {
        timeLeft -= 5* Time.deltaTime;
        Debug.Log(timeLeft);
        if (timeLeft < 0f) {
            for (int i = 0; i < 5; i++) {
                ExpController.instance.SpawnExp(transform.position);
            }
            timeLeft = time;
        }
    }

}
