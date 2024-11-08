using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseFacade : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuObject;

    [SerializeField]
    private GameObject ControlsMenuObject;
    /*
    [SerializeField]
    private GameObject SkillMenuObject;
    [SerializeField]
    private GameObject ItemMenuObject;
    */

    [SerializeField]
    public Button PlayButton;

    [SerializeField]
    private Button ControlsButton;

    [SerializeField]
    private Button ResetButton;

    [SerializeField]
    private Button BCModeButton;

    [SerializeField]
    private GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        ResumeGame(); // game starts UNPAUSED and the dog hides
        Debug.Log("hi chat");


        PlayButton.onClick.AddListener(ResumeGame);
        ControlsButton.onClick.AddListener(ShowControls);
        ResetButton.onClick.AddListener(ResetGame);
        BCModeButton.onClick.AddListener(StartBC);
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
        ControlsMenuObject.SetActive(false);
        PauseMenuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("resumed");
    }

    public void ShowControls()
    {
        ControlsMenuObject.SetActive(true);
    }

    public void ResetGame()
    {
        Debug.Log("we tried ok");
        gm.RestartGame("MinimumViableProduct");
    }

    public void StartBC()
    {
        Debug.Log("starting in BC mode...");
        // I have no idea what to do here
    }
}

