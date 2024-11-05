using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private GameObject UpgradeShopMenuObject;
    [SerializeField] private GameObject WeaponShopMenuObject;
    [SerializeField] private GameObject BuffShopMenuObject;

    private void Start()
    {
        UpgradeShopMenuObject.SetActive(false);
        WeaponShopMenuObject.SetActive(false);
        BuffShopMenuObject.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        UpgradeShopMenuObject.SetActive(false);
        WeaponShopMenuObject.SetActive(false);
        BuffShopMenuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("resumed");
    }

    public void ResumeGame(GameObject MenuObject)
    {
        Time.timeScale = 1;
        MenuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("resumed");
    }
}
