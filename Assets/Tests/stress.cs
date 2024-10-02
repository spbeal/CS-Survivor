using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// namespace Tests {}

public class stress
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test_testSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

/*    [UnityTest]
    public IEnumerator movement_test_left()
    {
        yield return new WaitWhile(() => sceneLoaded == false);
        var Player = GameObject.Find("Player").GetComponent<Character_Movement>();
        float initialpos = Player.transform.position.x;
        float speed = -10f;
        for (int i = 0; i < 750; i++)
        {
            Player.rb.velocity = new Vector2(speed, 0);
            yield return null;
        }
        Assert.AreNotEqual(Player.transform.position.x, initialpos);
    }
*/
    [UnityTest]
    public IEnumerator stress_gun_test()
    {
        
    }
}
