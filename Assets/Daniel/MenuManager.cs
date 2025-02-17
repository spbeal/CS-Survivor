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
        //PauseGame(); // game starts paused
        ResumeGame(); // game starts UNPAUSED and the dog hides
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

    public void PauseGame(){
        Time.timeScale = 0;
        PauseMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("paused");
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        PauseMenuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("resumed");
    }
}

