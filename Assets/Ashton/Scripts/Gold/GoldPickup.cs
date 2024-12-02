using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{

    // variables
    private int _goldValue;

    private PlayerStats playerStats => PlayerStats.instance;


    void Start()
    {
        _goldValue = 1;
    }


    // methods
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            GoldController.instance.GetGold(_goldValue);

            Destroy(gameObject);
        }
    }

    public int GetGoldValue()
    {
        return _goldValue;
    }

}
