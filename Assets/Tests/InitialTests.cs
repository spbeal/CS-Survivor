using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class InitialTests
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

/*        [UnityTest]
        public IEnumerator RapidFireStressTest()
        { 
        }*/

        [UnityTest]
        public IEnumerator PauseFunctionTest()
        {
/*            GameObject player = GameObject.Instantiate(playerPrefab, playerPos, playerDir);

            Assert.That(player.GetComponent<Player>().health, Is.EqualTo(100f));

            player.GetComponent<Player>().applyDamage(20f);
*/
            yield return new WaitWhile(() => sceneLoaded == false);

            //var gobject2 = GameObject.Find("MenuManager");
            //var gobject = GameObject.Find("ManuManager").GetComponent<MenuManager>();
            //GameObject pausePrefab = Resources.Load<GameObject>("MenuManager");

            var gameSpeed = Time.timeScale;
           // GameObject go = GameObject.Find("somegameobjectname");

            Assert.AreEqual(gameSpeed, 1.0f);

            //Assert.That(pausePrefab.GetComponent<PauseGame()>());
            //gobject.PauseGame();

            gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 0f);

            //Assert.That(pausePrefab.GetComponent<MenuManager>().PauseGame());

            gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 1f);

            yield return null;

        }


        [UnityTest]
        public IEnumerator movement_test_right()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            //var player = GameObject.Find("Player").GetComponent<Player>();
/*            float initialpos = Player.transform.position.x;
            float speed = 10f;
            for (int i = 0; i < 750; i++)
            {
                Player.rb.velocity = new Vector2(speed, 0);
                yield return null;
            }
            Assert.AreNotEqual(Player.transform.position.x, initialpos);*/
        }

        [UnityTest]
        public IEnumerator movement_test_left()
        {
            yield return new WaitWhile(() => sceneLoaded == false);
            //var Player = GameObject.Find("Player").GetComponent<Movement>();
/*            float initialpos = Player.transform.position.x;
            float speed = -10f;
            for (int i = 0; i < 750; i++)
            {
                Player.rb.velocity = new Vector2(speed, 0);
                yield return null;
            }
            Assert.AreNotEqual(Player.transform.position.x, initialpos);*/
        }
    }
}
