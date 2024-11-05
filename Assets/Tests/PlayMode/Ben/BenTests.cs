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

        for(int i = 0; i < 50000000; i += 5000)
        {
            Gun.magazineSize = i;
            Gun.bulletsPerTap = i;

            Gun.Shoot();

            Gun.Reload();

            float fps = 30f;
            float newFPS = 1.0f / Time.unscaledDeltaTime;
            fps = Mathf.Lerp(fps, newFPS, 0.0005f);
            
            if(fps < 5f){
                Assert.Fail("The stress test reduced the frames to under 5.");
            }

            yield return null;
        }

        Assert.Pass("The stress test didn't majorly reduce frames!");
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
