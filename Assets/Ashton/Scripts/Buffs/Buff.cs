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
        playerStats.SetGoldRate(playerStats.GetGoldRate() + _goldRateIncrease);
    }
}

public class DamageBuff : Buff
{
    private int _damageIncrease;

    public DamageBuff(int damageIncrease)
    {
        _damageIncrease = damageIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetDamage(playerStats.GetDamage() + _damageIncrease);
    }
}

public class MagSizeBuff : Buff
{
    private int _magSizeIncrease;

    public MagSizeBuff(int magSizeIncrease)
    {
        _magSizeIncrease = magSizeIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetMagSize(playerStats.GetMagSize() + _magSizeIncrease);
    }
}

public class ReloadSpeedBuff : Buff
{
    private int _reloadSpeedIncrease;

    public ReloadSpeedBuff(int reloadSpeedIncrease)
    {
        _reloadSpeedIncrease = reloadSpeedIncrease;
    }

    public override void Apply(PlayerStats playerStats)
    {
        playerStats.SetReloadSpeed(playerStats.GetReloadSpeed() + _reloadSpeedIncrease);
    }
}



