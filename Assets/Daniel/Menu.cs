using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu
{

}

public class ItemMenu : Menu
{

}

public class PauseMenu : Menu
{
    [SerializeField] GameObject PauseMenuObject;
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

<<<<<<< HEAD
public class MenuItem
{

}
=======
//public class MenuItem
>>>>>>> b3c952aed1bd647b8c8f9725a7eb817a896367aa
