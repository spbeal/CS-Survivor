using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private PlayerStats playerStats => PlayerStats.instance;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerStats.GetCurrentHealth() / playerStats.GetMaxHealth(), 0, 1);
    }
}
