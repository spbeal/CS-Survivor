using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void StartGame()
    {

    }
}
