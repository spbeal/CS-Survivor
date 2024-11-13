using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BenTests
{
    // whether the scene is loaded
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
        // make sure the scene is loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        // get the gun's gun system object to use
        var Gun = GameObject.Find("Belt-Fed_Shotgun").GetComponent<GunSystem>();

        // make sure the gun doesn't have to wait to reload
        Gun.reloadTime = 0;

        // we are going to make the gun fire 5000 more times every frame until it reaches an absurd number
        for(int i = 0; i < 50000000; i += 5000)
        {
            // iterate the magazine size (the amount of bullets the gun can fire) and the bullets per shot (the amount of bullets shot out of the gun per shot)
            Gun.magazineSize = i;
            Gun.bulletsPerTap = i;

            // shoot the gun
            Gun.Shoot();

            // make sure to always keep the gun stocked up with ammo
            Gun.Reload();

            // find the current frames per second
            float fps = 30f;
            float newFPS = 1.0f / Time.unscaledDeltaTime;
            fps = Mathf.Lerp(fps, newFPS, 0.0005f);
            
            // if the fps is less than 5, then the game failed to withstand the required amount of fires per frame.
            if(fps < 5f){
                Assert.Fail("The stress test reduced the frames to under 5.");
            }

            yield return null;
        }

        // if the game never went under 5 fps, then the game successfully withstood the required number of fires per frame.
        Assert.Pass("The stress test didn't majorly reduce frames!");
    }

    [UnityTest]
    public IEnumerator GunBoundaryTest1Ben()
    {
        // make sure the scene is loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        // get the gun's gun system object to use
        var Gun = GameObject.Find("Belt-Fed_Shotgun").GetComponent<GunSystem>();

        // make sure the gun doesn't have to wait to reload and set magazineSize and bulletsPerTap to a fixed amount to make sure they are valid
        Gun.reloadTime = 0;
        Gun.magazineSize = 20;
        Gun.bulletsPerTap = 1;

        // shoot the gun until it is supposed to be empty
        for(int i = 0; i < Gun.magazineSize; i++)
        {
            Gun.Shoot();
            yield return null;
        }

        // reload the gun
        Gun.Reload();

        // check if reloading works or if the new amount of bullets after reloading is different then what it should have
        if(Gun.bulletsLeft == Gun.magazineSize)
        {
            Assert.Pass("The Gun reloaded as expected. Bullets left out of magazine size: " + Gun.bulletsLeft + " / " + Gun.magazineSize);
        }
        Assert.Fail("The Gun did not reload as expected. Bullets left out of magazine size: " + Gun.bulletsLeft + " / " + Gun.magazineSize);
    }

    [UnityTest]
    public IEnumerator GunBoundaryTest2Ben()
    {
        // make sure the scene is loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        // get the gun's gun system object to use
        var Gun = GameObject.Find("Belt-Fed_Shotgun").GetComponent<GunSystem>();

        // make sure the gun doesn't have to wait to reload and set magazineSize and bulletsPerTap to a fixed amount to make sure they are valid
        Gun.reloadTime = 0;
        Gun.magazineSize = 5;
        Gun.bulletsPerTap = 1;

        // shoot the gun a couple times to check if it has the correct amount of bullets left afterwards
        for(int i = 0; i < 2; i++)
        {
            Gun.Shoot();
            yield return null;
        }

        // check if the gun now has the correct amount of bullets left
        if(Gun.bulletsLeft > 3)
        {
            Assert.Fail("The Gun was not fired as expected. Bullets left: " + Gun.bulletsLeft);
        }
        Assert.Pass("The Gun was fired as expected. Bullets left: " + Gun.bulletsLeft);
    }

    [UnityTest]
    public IEnumerator AreGameManagersObjectsNull()
    {
        // make sure the scene is loaded
        yield return new WaitWhile(() => sceneLoaded == false);

        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(gameManager.playerStats == null){
            Assert.Fail("One of the objects in GameManager was not initialized which will cause it to no longer be able to function! This is most likely caused by the name of the needed object in the scene being different than what GameManager.cs has recorded. The first, and possibly last, null object: playerStats");
        }

        if(gameManager.lightSystem == null){
            Assert.Fail("One of the objects in GameManager was not initialized which will cause it to no longer be able to function! This is most likely caused by the name of the needed object in the scene being different than what GameManager.cs has recorded. The first, and possibly last, null object: lightSystem");
        }

        if(gameManager.enemySpawner == null){
            Assert.Fail("One of the objects in GameManager was not initialized which will cause it to no longer be able to function! This is most likely caused by the name of the needed object in the scene being different than what GameManager.cs has recorded. The first, and possibly last, null object: enemySpawner");
        }

        Assert.Pass("None of the objects are null, GameManager is good to go!");
    }
}
