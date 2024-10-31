using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu
{
    public virtual void DefaultBehavior() // called when <space> pressed, probably
    {
        this.SetActive(false);
        time.timeScale = 1;
    }
}

public class ItemMenu : Menu
{
    public override void DefaultBehavior()
    {
        for(int i = 0; i < MenuItems.Length; i++){
            if(MenuItems[i].Selected){
                MenuItems[i].Buy();
            }
        }
        Time.timeScale = 1;
        this.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
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

    public override void DefaultBehavior()
    {
        ResumeGame();
    }
}

public class MenuItem
{
    string Name;
    int Price;
    bool Selected;
    int DamageModifier;
    int HealthModifier;
    int ArmorModifier;
    int GetPrice()
    {
        return this.Price;
    }
    void Buy(){
        /*
        if(Player.Money < this.Price){
            break;
        }
        Player.Money -= this.Price;
        Player.Health += this.HealthModifier;
        // etc
        */
    }
}
