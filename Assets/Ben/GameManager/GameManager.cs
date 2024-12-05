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

    // object to fill with the win menu so that we can turn it on or off
    [SerializeField] // needs to be filled by drag and drop in the unity menu because doing it by code is real sketchy for some reason
    private GameObject winMenu;

    // whether we are able to start another round or not
    private bool startNextRound;
    // whether the round has already ended or not
    private bool roundEnded;
    // used to make the lights flash at the end of the break phase after a round ends
    private bool waitIsOver;

    // the instance of itself to use in order to implement the singleton pattern
    private GameManager instance;

    [SerializeField]
    private int currentRound = 1; // Stores the number of the current round (Start at round 1)
    [SerializeField]
    private int maxRounds = 5; // Limits the game to 5 rounds
    [SerializeField]
    private int enemiesPerRound = 3; // Number of enemies to spawn per round

    // the private constructor in order to make sure no other class can call the constructor
    private GameManager()
    {
        startNextRound = false;
        roundEnded = false;
        waitIsOver = false;
    }

    // this is the function needed to be called in order to get the GameManager object in other classes (due to singleton pattern)
    public GameManager getInstance()
    {
        // if there isn't already an instance of GameManager, create one
        if(instance.instance == null){
            instance.instance = new GameManager();
        }
        // send back the instance of GameManager
        return instance.instance;
    }

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
    }

    // Update is called once per frame
    void Update()
    {
        // if we should start another round, then spawn another wave of enemies
        if(startNextRound && currentRound <= maxRounds){
            // make sure we don't immediatly start another round again
            startNextRound = false;
            // spawn a wave of enemies
            SpawnEnemies();
            // Move to the next round
            currentRound++;
        }else if(currentRound > maxRounds){ // this is here for catching any cases where the currentRound becomes greater than the maxRounds and wasn't already caught by the equals check when the round ends
            winMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // check if there is an enemy in the scene
        isThereEnemy = GameObject.FindWithTag("Enemy");

        // if there are no enemies left in the scene, then end the round. Also checks whether the round has already ended or not to make sure we don't start multiple rounds at once
        if( isThereEnemy == null && Time.time > 5 && !roundEnded ){
            Debug.Log("round has ended");
            roundEnded = true;
            // if we are going to be over the max rounds after this waiting period, just end the game then and there since there's no wave afterwards anyway
            if(currentRound == maxRounds){
                winMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
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

    public void ActivateBcMode()
    {
        playerStats.SetMaxHealth(999999);
        playerStats.SetCurrentHealth(999999);
        playerStats.SetSpeed(30f);
        playerStats.SetSpeed(30f);
        playerStats.SetGold(999999);
        playerStats.SetGoldRate(999999);
        playerStats.SetDamage(99999);
        playerStats.SetMagSize(99999);
        playerStats.SetReloadSpeed(0.0f);
    }

    // the coroutine that is called when a round ends
    IEnumerator endRound()
    {
        // give the player some gold for completing the round without dying
        playerStats.AddGold(20);
        // there is a 30 second break between each wave
        yield return new WaitForSeconds(25);
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
        Debug.Log($"Starting round {currentRound}");

        int enemiesToSpawn = enemiesPerRound * currentRound;

        // add the correct amount of enemies and enemy types for the wave
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            string enemyType = enemySpawner.GetWeightedEnemyType(currentRound); // Get the enemy type based on the current round
            enemySpawner.SpawnEnemy(enemyType);
        }
    }

    /* Just in case assembly defs keep not working and if I need a backup way to solve it
    public bool CheckIfDie()
    {
        int health = playerStats.GetCurrentHealth();
        if(health <= 0){
            return true;
        }else{
            return false;
        }
    }
    */

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
