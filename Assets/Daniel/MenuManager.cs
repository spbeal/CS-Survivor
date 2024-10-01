using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuObject;
    /*
    [SerializeField]
    private GameObject SkillMenuObject;
    [SerializeField]
    private GameObject ItemMenuObject;
    */

    [SerializeField]
    private Button PlayButton;

    // Start is called before the first frame update
    void Start()
    {
        PauseGame(); // game starts paused
        //SkillMenuObject.SetActive(false);
        //ItemMenuObject.SetActive(false);
        Debug.Log("hi chat");

        PlayButton.onClick.AddListener(ResumeGame);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape") && Time.timeScale == 1){
            PauseGame();
        } else if(Input.GetKeyDown("escape") && Time.timeScale == 0){
            ResumeGame();
        }
    }

    void PauseGame(){
        Time.timeScale = 0;
        PauseMenuObject.SetActive(true);
        Debug.Log("paused");
    }

    void ResumeGame(){
        Time.timeScale = 1;
        PauseMenuObject.SetActive(false);
        Debug.Log("resumed");
    }
}

