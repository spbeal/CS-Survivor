using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObject;

    // Start is called before the first frame update
    void Start()
    {
        menuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame(){
        Time.timeScale = 0;
        menuObject.SetActive(true);
    }

    public void resumeGame(){
        Time.timeScale = 1;
        menuObject.SetActive(false);
    }
}
