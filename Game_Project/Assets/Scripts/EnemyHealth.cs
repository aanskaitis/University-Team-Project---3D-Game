using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class represents the enemy's health. 
/// 
/// </summary>

public class EnemyHealth : MonoBehaviour {

    public GameObject self; // Reference to the enemy this script is attached to
    public Image healthBar; // Reference to the UI health bar above the enemy
    public EnemyManager enemyManager; // Reference to manager to keep track of the number of enemies
    public float maxHealth = 100f; // Starting health of the enemy
    private int moneyDroppedOnKill = 10; // Quantity of money the player recieves upon this enemy dying

    private PlayerStats playerStats; // Used to increase player money upon dying
    private float health; // Represents the real time amount of health the enemy has

    [Header("UI Health Bar Attrs")]
    private float maxWidth; // Starting width of the green bar
    private Vector2 deltaSize; // Size of the health bar
    private Vector3 coords; // Location of the health bar

    void Start() {
        // Set health, store starting width, starting size and create a ref. to the player's stats
        health = maxHealth;
        maxWidth = healthBar.rectTransform.rect.width;
        deltaSize = healthBar.rectTransform.sizeDelta;
        playerStats = self.GetComponent<enemyMovement>().player.GetComponent<PlayerStats>();

    }
    
    public void damage(float damage) {
        // Method called when the enemy is damaged, enemy loses specified amount of health 
        this.health = health - damage;
    }

    private void death() {
        /* Called when the enemy has 0 or less health, player recieves money,
         * enemy object is garbage collected, number of enemies alive is decreased */
        playerStats.increaseMoney(moneyDroppedOnKill + Random.Range(1, 5));
        Destroy(self);
        enemyManager.decreaseCurrentAlive();

    }

    private void drawHealthBar() {
        // This method draws the health bar above the enemy object
        deltaSize[0] = (health / maxHealth) * maxWidth;

        if (deltaSize[0] != maxWidth) {
            coords[0] = (deltaSize[0] / 2) - 0.6f;
        }

        healthBar.rectTransform.sizeDelta = deltaSize;
        healthBar.rectTransform.anchoredPosition = coords;

    }

    void Update() {
        // Checks if the enemy should be dead
        if (this.health <= 0f) {
            this.death();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Enemy_Death", GetComponent<Transform>().position);
        }
        // updates the coordinates of the health bar to be above the enemy
        deltaSize = healthBar.rectTransform.sizeDelta;
        coords = healthBar.rectTransform.anchoredPosition;
        drawHealthBar();

    }

}
