using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class represents the player's stats. Specifically the player's
/// money and health. This class contains methods to decrease/increase 
/// the player's stats for other interacting objects. For example, when 
/// the enemy hits the player, the damagePlayer method is called to decrease
/// the player's health.
/// </summary>

public class PlayerStats : MonoBehaviour
{

    // healthText modifies on screen value to communicate to user their health
    public Text healthText; // Display's player health on screen
    private int health; // Stores player health points

    public Text moneyText; // Displays player's money on screen during combat
    public Text moneyTextBuildPhase; // Display's player money on screen during building
    public int money; // Stores player's money


    void Start() {
        // Set health and money
        health = 100;
        money = 0;
    }

    public void updateHealthText() {
        // Updates on screen value of health
        healthText.text = health.ToString();
    }

    public void updateMoneyText() {
        // Updates on screen value of money
        moneyText.text = money.ToString();
        moneyTextBuildPhase.text = money.ToString();
    }

    // Called when an enemy hits the player
    public void damagePlayer(int damage) {
        // Decreases the player health, called when hit by an enemy
        health = health - damage;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player_Damage", GetComponent<Transform>().position);
        updateHealthText();

        // If player health is equal to or less than 0 call player died method
        if (health <= 0) {
            playerDied();
        }

    }

    public void increaseMoney(int _money) {
        // Increases money stat, called when the player kills an enemy
        money = money + _money;
        updateMoneyText();
    }

    
    public void playerDied() {
        // Opens the game manager if player is dead
        FindObjectOfType<GameManager>().EndGame();

    }

}
