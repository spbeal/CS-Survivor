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

    public GoldPickup pickup;

    public void SpawnGold(Vector3 position)
    {
        for (int i = 0; i < Random.value * 3 + 1; i++)
        {
            // instantiate gold pickup
            GoldPickup spawnedGold = Instantiate(pickup, position + new Vector3((Random.value - 0.5f) * 2, (Random.value - 0.5f) * 2, (Random.value - 0.5f) * 2), Quaternion.identity) as GoldPickup;
            // set a despawn timer to that instantiation
            //Destroy(spawnedGold, 8f);
        }

    }

}
