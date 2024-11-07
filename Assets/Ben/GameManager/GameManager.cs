using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerStats playerStats;

    private LightSystem lightSystem;

    private GameObject isThereEnemy;

    private bool startNextRound = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        lightSystem = GameObject.Find("LightManager").GetComponent<LightSystem>();
        // spawn a wave of enemies here
    }

    // Update is called once per frame
    void Update()
    {
        isThereEnemy = GameObject.FindWithTag("Enemy");

        if( isThereEnemy == null && Time.time > 1 ){
            StartCoroutine( endRound() );
        }

        if(startNextRound){
            startNextRound = false;
            // spawn a wave of enemies here
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
        yield return new WaitForSeconds(25);
        // flash the lights 5 seconds before the next round starts
        lightSystem.Update_SingleLights( true, true );
        yield return new WaitForSeconds(5);
        // turn them back to regular when the round starts
        lightSystem.Update_SingleLights( true, false );
        startNextRound = true;
    }
}
