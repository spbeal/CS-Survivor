using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BenTests
{
    private bool sceneLoaded;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene("Main/MinimumViableProduct", LoadSceneMode.Single);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sceneLoaded = true;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GunStressTestBen()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        var Gun = GameObject.Find("Brick").GetComponent<GunSystem>();

        Gun.reloadTime = 0;

        for(int i = 0; i < 500000; i += 500)
        {
            Gun.magazineSize = i;
            Gun.bulletsPerTap = i;

            Gun.Shoot();

            Gun.Reload();

            yield return null;
        }

        Assert.Fail("The stress test failed to break the game");
    }

    [UnityTest]
    public IEnumerator GunBoundaryTest1Ben()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        var Gun = GameObject.Find("Brick").GetComponent<GunSystem>();

        Gun.reloadTime = 0;
        Gun.magazineSize = 20;
        Gun.bulletsPerTap = 1;

        for(int i = 0; i < Gun.magazineSize; i++)
        {
            Gun.Shoot();
            yield return null;
        }

        Gun.Reload();

        if(Gun.bulletsLeft == Gun.magazineSize)
        {
            Assert.Pass("The Gun reloaded as expected.");
        }
        Assert.Fail("The Gun did not reload as expected. Bullets left: " + Gun.bulletsLeft);
    }

    [UnityTest]
    public IEnumerator GunBoundaryTest2Ben()
    {
        yield return new WaitWhile(() => sceneLoaded == false);

        var Gun = GameObject.Find("Brick").GetComponent<GunSystem>();

        Gun.reloadTime = 0;
        Gun.magazineSize = 5;
        Gun.bulletsPerTap = 1;

        for(int i = 0; i < 2; i++)
        {
            Gun.Shoot();
            yield return null;
        }

        if(Gun.bulletsLeft > 3)
        {
            Assert.Fail("The Gun was not fired as expected. Bullets left: " + Gun.bulletsLeft);
        }
        Assert.Pass("The Gun was fired as expected.");
    }
}
