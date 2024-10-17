using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{

    // create an isntance of the class
    public static ExpController instance;
    private void Awake() {
        instance = this;
    }


    // variables
    public int currentExp;

    public ExpPickup pickup;


    // methods
    public void GetExp(int amountToGet) {
        currentExp += amountToGet;
        Debug.Log(currentExp);
    }

    public void SpawnExp(Vector3 position) {
        Instantiate(pickup, position + new Vector3((Random.value-0.5f)*2, (Random.value-0.5f)*2, (Random.value-0.5f)*2), Quaternion.identity);

        // TODO: inside the enemy controller:
        //
        //  if (enemy dies) {
        //      ExpController.instance.SpawnExp(transform.position);
        //  }

        // TODO: add a despawn timer.

    }

}
