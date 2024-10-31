using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_EnemyBehavior : EnemyBehavior
{
    public float pushBackForce = 5f; //Force to push player back

    public override void Attack()
    {
        //Custom push-back attack
        if (playerInAttackRange)
        {
            Debug.Log("Advanced Enemy performs a push-back attack.");

            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDirection = (player.position - transform.position).normalized;
                playerRb.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);
            }
        }
    }
}
