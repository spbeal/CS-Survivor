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

    private HealthSkill healthSkill;
    private MovementSpeedSkill movementSpeedSkill;
    private PlayerStats playerStats;

    public bool HasEnoughPoints(Skills skill, int requiredPoints)
    {
        return true;
    }

    public void UpgradeSkill(Skills skill)
    {
        switch (skill)
        {
            case Skills.Health:
                healthSkill.Upgrade();
                break;
            case Skills.MovementSpeed:
                movementSpeedSkill.Upgrade();
                break;
        }
        Debug.Log(skill.ToString() + " upgraded!");
    }

}


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
        if (_playerCurrentPoints < _requiredPoints) {
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
        if (_playerCurrentPoints < _requiredPoints) {
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

