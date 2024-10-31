using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{

    // variables
    private int _expValue;

    void Start()
    {
        _expValue = 1;
    }


    // methods
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            ExpController.instance.GetExp(_expValue);

            Destroy(gameObject);
        }
    }

    public int GetExpValue()
    {
        return _expValue;
    }

}
