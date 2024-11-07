using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LossScript : MonoBehaviour
{
    [SerializeField]
    private GameObject LossMenu;

    [SerializeField]
    private Button ReplayButton;

    [SerializeField]
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        ReplayButton.onClick.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetGame()
    {
        gm.RestartGame("MinimumViableProduct");
    }

    public void ActivateLossScreen()
    {
        Time.timeScale = 0;
        LossMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
