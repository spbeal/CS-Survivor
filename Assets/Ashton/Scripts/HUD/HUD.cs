using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    private PlayerStats playerStats => PlayerStats.instance;
    private GunSystem gunSystem => GunSystem.instance;

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI ammoCount;
    [SerializeField] private TextMeshProUGUI goldText;

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerStats.GetCurrentHealth() / playerStats.GetMaxHealth(), 0, 1);
        goldText.text = playerStats.GetGold().ToString();
        ammoCount.text = gunSystem.bulletsLeft.ToString() + " / " + playerStats.GetMagSize().ToString();
    }
}
