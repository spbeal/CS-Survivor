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

    // used to play audio for wilder voice clips
    [SerializeField]
    private AudioSource wilderVoice;
    // used to play audio for dr.BC voice clips
    [SerializeField]
    private AudioSource bcVoice;
    // used to play audio for bolden voice clips
    [SerializeField]
    private AudioSource boldenVoice;

    private bool turnedOnWilder = false;

    // audio clips for wilder
    [SerializeField]
    private AudioClip hello1Wilder;
    [SerializeField]
    private AudioClip bye1Wilder;

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

        if(UpgradeShopMenuObject.activeSelf){
            turnedOnWilder = true;
        }else if(turnedOnWilder && !UpgradeShopMenuObject.activeSelf){
            wilderVoice.PlayOneShot(bye1Wilder, 0.6f);
            turnedOnWilder = false;
        }
    }

    private void Interact()
    {
        // add interaction logic
        Debug.Log("Interacted with " + gameObject.name);
        if (gameObject.name == "UpgradeShopCollider")
        {
            // play the hello voice clip when first interacting with Wilder
            if(!UpgradeShopMenuObject.activeSelf){
                wilderVoice.PlayOneShot(hello1Wilder, 1.3f);
            }
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
