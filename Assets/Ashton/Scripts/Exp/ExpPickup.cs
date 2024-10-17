using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{

    // variables
    public int expValue;


    // methods
    private void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
            ExpController.instance.GetExp(expValue);

            Destroy(gameObject);
        }
    }

}
