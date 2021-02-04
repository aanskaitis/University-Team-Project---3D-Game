using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class contains the behaviour for the point to defend.
/// The player must prevent enemies from walking to and hitting this point in the level.
/// When an enemy does do this, the point to defend loses health. The health of this object
/// is also rendered in game as text.
/// </summary>

public class PointToDefend : MonoBehaviour
{

    public Text healthText; // In-game text to for the player to see the health
    public int maxHealth = 100; // Starting health
    private int health; // Actual in-game health

    // Start is called before the first frame update
    void Start() {
        // Set health, update text
        health = maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public void Damage(int damage) {
        // Decrease health by amount, damage, update text, check if point to defend 
        // has been destroyed
        health = health - damage;
        CheckHealth();
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    private void CheckHealth() {
        // If the health is less than 0 then end the game
        if (health < 0) {
            FailLevel();
        }
    }

    private void FailLevel() {
        // Loads end game screen
        FindObjectOfType<GameManager>().EndGame();

    }

}
