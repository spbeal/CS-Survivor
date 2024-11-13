using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // --- variables for the player stats
    // the maximum health for the player
    [SerializeField]
    private int _maxHealth = 100;
    // the health that the player currently has
    [SerializeField]
    private int _currentHealth;

    // the speed of the player
    [SerializeField]
    private float _speed = 10f;

    // the currency that the player has to buy upgrades with
    [SerializeField]
    private int _gold = 0;

    // *****Ashton please tell me what this is*****
    [SerializeField]
    private int _goldRate = 0;

    public static PlayerStats instance;
    private void Awake()
    {
        instance = this;
        _currentHealth = _maxHealth; // Initialize health
    }

    // makes the player take the given damage and checks if that makes them go into negative values of health (meaning they should die and the game should end)
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // meant to be used when using a health pack that you should get from bolden's buff shop that refills your health
    public void ResetHealth(){
        _currentHealth = _maxHealth;
    }

    // kills the player and displays the lose screen
    private void Die()
    {
        Debug.Log("Player has died!");
        // display the death screen here with the button that will call the Game Manager's RestartGame function
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // --- get methods to return the value
    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public int GetGold()
    {
        return _gold;
    }

    public int GetGoldRate()
    {
        return _goldRate;
    }



    // --- set methods to change the value of the stats
    public void SetMaxHealth(int newHealth)
    {
        _maxHealth = newHealth;
    }

    public void SetCurrentHealth(int newHealth)
    {
        _currentHealth = newHealth;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void SetGold(int newGold)
    {
        _gold = newGold;
    }

    public void SetGoldRate(int newGoldRate)
    {
        _goldRate = newGoldRate;
    }



    // methods to add a value to the stats
    public void AddGold(int moreGold)
    {
        _gold += moreGold;
    }


}
