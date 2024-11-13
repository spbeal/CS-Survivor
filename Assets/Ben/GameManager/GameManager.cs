using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // used to give gold to the player at the end of each round
    public PlayerStats playerStats;

    // used to change the lights from regular white to colored and flashing
    public LightSystem lightSystem;

    // used for spawning enemies and part of the round system
    public EnemySpawner enemySpawner;
    
    // object to fill if there is an enemy currently present in the scene
    private GameObject isThereEnemy;

    // whether we are able to start another round or not
    private bool startNextRound = false;
    // whether the round has already ended or not
    private bool roundEnded = false;
    // used to make the lights flash at the end of the break phase after a round ends
    private bool waitIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // get the player statistics object and check if it was successfully gotten
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if(playerStats == null){
            Debug.Log("PlayerStats is NULL!");
        }

        // get the light system object and check if it was successfully gotten
        lightSystem = GameObject.Find("LightManager").GetComponent<Lights>().light_system;
        if(lightSystem == null){
            Debug.Log("lightSystem is NULL!");
        }

        // get the enemy spawner object and check if it was successfully gotten
        enemySpawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        if(enemySpawner == null){
            Debug.Log("enemySpawner is NULL!");
        }

        // spawn a wave of enemies
        SpawnEnemies();
        // Move to the next round
        enemySpawner.currentRound++; 
    }

    // Update is called once per frame
    void Update()
    {
        // if we should start another round, then spawn another wave of enemies
        if(startNextRound){
            // make sure we don't immediatly start another round again
            startNextRound = false;
            // spawn a wave of enemies
            SpawnEnemies();
            // Move to the next round
            enemySpawner.currentRound++;
        }

        // check if there is an enemy in the scene
        isThereEnemy = GameObject.FindWithTag("Enemy");

        // if there are no enemies left in the scene, then end the round. Also checks whether the round has already ended or not to make sure we don't start multiple rounds at once
        if( isThereEnemy == null && Time.time > 5 && !roundEnded ){
            Debug.Log("round has ended");
            roundEnded = true;
            StartCoroutine( endRound() );
        }

        // this function needs to be run every frame in order to make the lights flash and pulse, so my solution was to put this here 
        // and change the value of waitIsOver in the endRound coroutine when the function should be called every frame and when it should stop being called
        if(waitIsOver){
            lightSystem.Update_SingleLights( true, true );
        }
    }

    // function that can be called from anywhere that simply reloads the requested scene (level) or if no scene is given, MinimumViableProduct will be reloaded. 
    public void RestartGame( string? sceneName )
    {
        if( !string.IsNullOrEmpty(sceneName) ){
            SceneManager.LoadScene( sceneName );
        }else{
            SceneManager.LoadScene( "MinimumViableProduct" );
        }
    }

    // the coroutine that is called when a round ends
    IEnumerator endRound()
    {
        // give the player some gold for completing the round without dying
        playerStats.AddGold(100);
        // there is a 30 second break between each wave
        yield return new WaitForSeconds(5);
        // flash the lights 5 seconds before the next round starts
        waitIsOver = true;
        // 5 seconds of lights flashing
        yield return new WaitForSeconds(5);
        // turn them back to regular when the round starts
        waitIsOver = false;
        lightSystem.Update_SingleLights( true, false );
        // let the next round start
        startNextRound = true;
        // let the new round end too
        roundEnded = false;
    }
    
    // spawns a wave of enemies using the enemySpawner object
    private void SpawnEnemies()
    {
        // put this into a variable with a much smaller name
        int round = enemySpawner.currentRound;

        Debug.Log($"Starting round {round}");

        // add the correct amount of enemies and enemy types for the wave
        for (int i = 0; i < enemySpawner.enemiesPerRound; i++)
        {
            string enemyType = enemySpawner.GetWeightedEnemyType(round); // Get the enemy type based on the current round
            enemySpawner.SpawnEnemy(enemyType);
        }
    }

    // scrapped coroutine to flash the lights, left this here just in case the code was useful later
    /*
    IEnumerator flashLights()
    {
        Debug.Log("in flashlights");
        float timeToStop = Time.time + 5;
        while( Time.time < timeToStop ){
            lightSystem.Update_SingleLights( true, true );
            Debug.Log("in while loop");
        }
        yield return new WaitForSeconds(1);
    }
    */
}
