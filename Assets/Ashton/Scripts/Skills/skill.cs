using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public virtual void Apply(PlayerStats playerStats) { }
}


public class Health : Buff
{
    private int _healthIncrease;

    public Health(int healthIncrease)
    {
        _healthIncrease = healthIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetMaxHealth(playerStats.GetMaxHealth() + _healthIncrease);
    }
}


public class Speed : Buff
{
    private int _speedIncrease;

    public Speed(int speedIncrease)
    {
        _speedIncrease = speedIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetSpeed(playerStats.GetSpeed() + _speedIncrease);
    }
}


