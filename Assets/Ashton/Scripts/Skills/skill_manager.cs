using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{

    /* SKILLS --
     *  health
     *  stamina
     *  move speed
     *  exp rate
    */


    public enum Buffs
    {
        Health,
        MovementSpeed,
    }

    private PlayerStats playerStats;

    private Buff healthBuff;
    private Buff speedBuff;

    public BuffManager(PlayerStats stats)
    {
        playerStats = stats;

        healthBuff = new Health(10);
        speedBuff = new Speed(1);
    }

    public bool HasEnoughPoints(Buffs buff, int requiredPoints)
    {
        return playerStats.GetGold() >= requiredPoints;
    }

    public void UpgradeBuff(Buffs buff)
    {
        switch (buff)
        {
            case Buffs.Health:
                healthBuff.Apply(playerStats);
                break;
            case Buffs.MovementSpeed:
                speedBuff.Apply(playerStats);
                break;
            default:
                Debug.LogError("Buff not found!");
                return;
        }
        Debug.Log(buff.ToString() + " upgraded!");
    }

}
