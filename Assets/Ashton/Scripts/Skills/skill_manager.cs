using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{

    /* SKILLS --
     *  health
     *  stamina
     *  move speed
     *  exp rate
    */


    public enum Skills
    {
        Health,
        MovementSpeed,
    }

    private PlayerStats playerStats;

    private Skill healthSkill;
    private Skill speedSkill;

    public SkillManager(PlayerStats stats)
    {
        playerStats = stats;

        healthSkill = new HealthBoost(10);
        speedSkill = new SpeedBoost(1);
    }

    public bool HasEnoughPoints(Skills skill, int requiredPoints)
    {
        return playerStats.GetSkillPoints() >= requiredPoints;
    }

    public void UpgradeSkill(Skills skill)
    {
        switch (skill)
        {
            case Skills.Health:
                healthSkill.Apply(playerStats);
                break;
            case Skills.MovementSpeed:
                speedSkill.Apply(playerStats);
                break;
            default:
                Debug.LogError("Skill not found!");
                return;
        }
        Debug.Log(skill.ToString() + " upgraded!");
    }

}

/*
public class HealthSkill : SkillManager
{
    private int _requiredPoints;
    private int _maxHealth;
    private int _playerCurrentPoints;

    private PlayerStats playerStats;

    public HealthSkill()
    {
        _requiredPoints = 1;
        _maxHealth = playerStats.GetMaxHealth();
        _playerCurrentPoints = playerStats.GetSkillPoints();
    }

    public void Upgrade()
    {
        if (_playerCurrentPoints < _requiredPoints)
        {
            Debug.Log("Not enough points to upgrade MaxHealth");
        }
        else
        {
            playerStats.SetSkillPoints(_playerCurrentPoints - _requiredPoints);
            _requiredPoints++;
            _maxHealth++;
            playerStats.SetMaxHealth(_maxHealth);
        }
    }
}


public class MovementSpeedSkill : SkillManager
{
    private int _requiredPoints;
    private int _movementSpeed;
    private int _playerCurrentPoints;

    private PlayerStats playerStats;

    public MovementSpeedSkill()
    {
        _requiredPoints = 1;
        _movementSpeed = playerStats.GetSpeed();
        _playerCurrentPoints = playerStats.GetSkillPoints();
    }

    public void Upgrade()
    {
        if (_playerCurrentPoints < _requiredPoints)
        {
            Debug.Log("Not enough points to upgrade MovementSpeed");
        }
        else
        {
            playerStats.SetSkillPoints(_playerCurrentPoints - _requiredPoints);
            _requiredPoints++;
            _movementSpeed++;
            playerStats.SetSpeed(_movementSpeed);
        }
    }
}
*/

