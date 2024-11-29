using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    /* SKILLS --
     *  health
     *  move speed
     *  gold rate
    */


    public enum Buffs
    {
        Health,
        MovementSpeed,
        GoldRate,
        Damage,
    }

    private PlayerStats playerStats;

    private Buff healthBuff;
    private Buff speedBuff;
    private Buff goldRateBuff;
    private Buff damageBuff;

    void Start()
    {
        healthBuff = new HealthBuff(10);
        speedBuff = new SpeedBuff(1);
        goldRateBuff = new GoldRateBuff(1);
        damageBuff = new DamageBuff(1);
    }

    /*
    public BuffManager(PlayerStats stats)
    {
        playerStats = stats;

        healthBuff = new HealthBuff(10);
        speedBuff = new SpeedBuff(1);
    }
    */

    public bool HasEnoughPoints(Buffs buff, int requiredGold)
    {
        return playerStats.GetGold() >= requiredGold;
    }

    public void UpgradeBuff(string buff)
    {
        switch (buff)
        {
            case "Health":
                healthBuff.Apply(playerStats);
                break;
            case "MovementSpeed":
                speedBuff.Apply(playerStats);
                break;
            case "GoldRate":
                goldRateBuff.Apply(playerStats);
                break;
            case "Damage":
                damageBuff.Apply(playerStats);
                break;
            case "MagSize":
                goldRateBuff.Apply(playerStats);
                break;
            case "ReloadSpeed":
                goldRateBuff.Apply(playerStats);
                break;
            default:
                Debug.LogError("Buff not found!");
                return;
        }
        Debug.Log(buff.ToString() + " upgraded!");
    }
}



