using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public float interactionRange = 2.0f;
    private Transform player;

    [SerializeField] private GameObject UpgradeShopMenuObject;
    [SerializeField] private GameObject WeaponShopMenuObject;
    [SerializeField] private GameObject BuffShopMenuObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        // add interaction logic
        Debug.Log("Interacted with " + gameObject.name);
        if (gameObject.name == "UpgradeShopCollider")
        {
            PauseGame(UpgradeShopMenuObject);
        }
        else if (gameObject.name == "WeaponShopCollider")
        {
            PauseGame(WeaponShopMenuObject);
        }
        else if (gameObject.name == "BuffShopCollider")
        {
            PauseGame(BuffShopMenuObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    public void PauseGame(GameObject MenuObject)
    {
        Time.timeScale = 0;
        MenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("paused");
    }
}
