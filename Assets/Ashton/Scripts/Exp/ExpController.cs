using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{

    // create an isntance of the class
    public static ExpController instance;
    private void Awake()
    {
        instance = this;
    }


    // variables
    private int _currentExp;

    public ExpPickup pickup;


    void Start()
    {
        _currentExp = 0;
    }


    // methods
    public void GetExp(int amountToGet)
    {
        _currentExp += amountToGet;
    }

    public void SpawnExp(Vector3 position)
    {
        ExpPickup spawnedExp = Instantiate(pickup, position + new Vector3((Random.value-0.5f)*2, (Random.value-0.5f)*2, (Random.value-0.5f)*2), Quaternion.identity) as ExpPickup;

        // TODO: inside the enemy controller:
        //
        //  if (enemy dies) {
        //      ExpController.instance.SpawnExp(transform.position);
        //  }

        Destroy(spawnedExp, 1f);
        // TODO: add a despawn timer.

    }

    public int GetCurrentExp()
    {
        return _currentExp;
    }

}
