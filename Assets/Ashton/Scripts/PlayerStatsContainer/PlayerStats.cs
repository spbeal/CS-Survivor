using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // to activate the loss screen on death
    [SerializeField]
    private GameObject lossMenu;

    // --- variables for the player stats
    // the maximum health for the player
    [SerializeField] private int _maxHealth;

    // the health that the player currently has
    [SerializeField] private int _currentHealth;

    // the speed of the player
    [SerializeField] private float _speed;

    // the currency that the player has to buy upgrades with
    [SerializeField] private int _gold;

    // the amount of gold that each gold pickup gives you once you collect it (these spawn when enemies are destroyed)
    [SerializeField] private int _goldRate;

    // the amount of damage the gun deals to enemies when it hits them
    [SerializeField] private int _damage = 20;

    // the maximum amount of bullets the gun can fire before needing to reload
    [SerializeField] private int _magSize = 10;

    // the amoun of time it takes to reload the gun (refill the bulletsLeft with magazineSize (in GunSystem.cs))
    [SerializeField] private float _reloadSpeed = 2.0f;

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
        Debug.Log("lossMenu: " + lossMenu);
        lossMenu.SetActive(true);
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

    public int GetDamage()
    {
        return _damage;
    }

    public int GetMagSize()
    {
        return _magSize;
    }

    public float GetReloadSpeed()
    {
        return _reloadSpeed;
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

    public void SetDamage(int newDamage)
    {
        _damage = newDamage;
    }

    public void SetMagSize(int newMagSize)
    {
        _magSize = newMagSize;
    }

    public void SetReloadSpeed(float newReloadSpeed)
    {
        _reloadSpeed = newReloadSpeed;
    }



    // methods to add a value to the stats
    public void AddGold(int moreGold)
    {
        _gold += moreGold;
    }


}
