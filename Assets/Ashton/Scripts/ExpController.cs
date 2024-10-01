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
    }

    public void SpawnExp(Vector3 position) {
        Instantiate(pickup, position, Quaternion.identity);

        // TODO: inside the enemy controller:
        //
        //  if (enemy dies) {
        //      ExpController.instance.SpawnExp(transform.position);
        //  }

    }

}
