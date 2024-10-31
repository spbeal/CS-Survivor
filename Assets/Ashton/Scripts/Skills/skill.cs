using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public virtual void Apply(PlayerStats playerStats);
}


public class HealthBoost : Skill
{
    private int _healthIncrease;

    public HealthBoost(int healthIncrease)
    {
        _healthIncrease = healthIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetMaxHealth(playerStats.GetMaxHealth() + _healthIncrease);
    }
}


public class SpeedBoost : Skill
{
    private int _speedIncrease;

    public SpeedBoost(int speedIncrease)
    {
        _speedIncrease = speedIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetSpeed(playerStats.GetSpeed() + _speedIncrease);
    }
}


