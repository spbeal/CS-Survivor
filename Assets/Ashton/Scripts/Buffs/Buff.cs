using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public virtual void Apply(PlayerStats playerStats) { }
}


public class HealthBuff : Buff
{
    private int _healthIncrease;

    public HealthBuff(int healthIncrease)
    {
        _healthIncrease = healthIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetMaxHealth(playerStats.GetMaxHealth() + _healthIncrease);
    }
}


public class SpeedBuff : Buff
{
    private int _speedIncrease;

    public SpeedBuff(int speedIncrease)
    {
        _speedIncrease = speedIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetSpeed(playerStats.GetSpeed() + _speedIncrease);
    }
}

public class GoldRateBuff : Buff
{
    private int _goldRateIncrease;

    public GoldRateBuff(int goldRateIncrease)
    {
        _goldRateIncrease = goldRateIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetSpeed(playerStats.GetSpeed() + _goldRateIncrease);
    }
}





