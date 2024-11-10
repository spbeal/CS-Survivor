using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{

    // create an isntance of the class
    public static GoldController instance;
    private void Awake()
    {
        instance = this;
    }


    // variables
    private int _currentGold;

    public GoldPickup pickup;


    void Start()
    {
        _currentGold = 0;
    }


    // methods
    public void GetGold(int amountToGet)
    {
        _currentGold += amountToGet;
    }

    public void SpawnGold(Vector3 position)
    {
        GoldPickup spawnedGold = Instantiate(pickup, position + new Vector3((Random.value-0.5f)*2, (Random.value-0.5f)*2, (Random.value-0.5f)*2), Quaternion.identity) as GoldPickup;

        // TODO: inside the enemy controller:
        //
        //  if (enemy dies) {
        //      GoldController.instance.SpawnGold(transform.position);
        //  }

        Destroy(spawnedGold, 1f);
        // TODO: add a despawn timer.

    }

    public int GetCurrentGold()
    {
        return _currentGold;
    }

}
