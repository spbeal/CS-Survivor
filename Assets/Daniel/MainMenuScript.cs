using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuObject;

    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button BCButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        BCButton.onClick.AddListener(StartBC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene(sceneName:"MinimumViableProduct");
    }

    void StartBC()
    {
        SceneManager.LoadScene(sceneName:"MinimumViableProduct");
    }
}
