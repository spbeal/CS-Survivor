using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerStats playerStats;

    private LightSystem lightSystem;

    private EnemySpawner enemySpawner;

    private GameObject isThereEnemy;

    private bool startNextRound = false;
    private bool roundEnded = false;
    private bool waitIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if(playerStats == null){
            Debug.Log("PlayerStats is NULL!");
        }

        lightSystem = GameObject.Find("LightManager").GetComponent<Lights>().light_system;
        if(lightSystem == null){
            Debug.Log("lightSystem is NULL!");
        }

        enemySpawner = GameObject.Find("EnemyManager").GetComponent<EnemySpawner>();
        if(enemySpawner == null){
            Debug.Log("enemySpawner is NULL!");
        }
        // spawn a wave of enemies
        SpawnEnemies();
        enemySpawner.currentRound++; // Move to the next round
    }

    // Update is called once per frame
    void Update()
    {
        if(startNextRound){
            startNextRound = false;
            // spawn a wave of enemies
            SpawnEnemies();
            enemySpawner.currentRound++; // Move to the next round
        }

        isThereEnemy = GameObject.FindWithTag("Enemy");

        if( isThereEnemy == null && Time.time > 5 && !roundEnded ){
            Debug.Log("round has ended");
            roundEnded = true;
            StartCoroutine( endRound() );
        }

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

    IEnumerator endRound()
    {
        playerStats.AddGold(10);
        // there is a 30 second break between each wave
        yield return new WaitForSeconds(5);
        // flash the lights 5 seconds before the next round starts
        waitIsOver = true;
        yield return new WaitForSeconds(5);
        waitIsOver = false;
        // turn them back to regular when the round starts
        lightSystem.Update_SingleLights( true, false );
        startNextRound = true;
        roundEnded = false;
    }

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
    
    private void SpawnEnemies()
    {
        int round = enemySpawner.currentRound;

        Debug.Log($"Starting round {round}");

        for (int i = 0; i < enemySpawner.enemiesPerRound; i++)
        {
            string enemyType = enemySpawner.GetWeightedEnemyType(round); // Get the enemy type based on the current round
            enemySpawner.SpawnEnemy(enemyType);
        }
    }
}
