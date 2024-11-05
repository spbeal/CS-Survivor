using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats instance;
    private void Awake()
    {
        instance = this;
    }

    // variables for the player stats
    private int _health;
    private int _speed;
    private int _skillPoints;

    private int _gold;



    // get methods to return the value
    public int GetMaxHealth()
    {
        return _health;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public int GetSkillPoints()
    {
        return _skillPoints;
    }

    public int GetGold()
    {
        return _gold;
    }



    // set methods to change the value of the stats
    public void SetMaxHealth(int newHealth)
    {
        _health = newHealth;
    }

    public void SetSpeed(int newSpeed)
    {
        _speed = newSpeed;
    }

    public void SetSkillPoints(int newSkillPoints)
    {
        _skillPoints = newSkillPoints;
    }

    public void SetGold(int newGold)
    {
        _gold = newGold;
    }


}
