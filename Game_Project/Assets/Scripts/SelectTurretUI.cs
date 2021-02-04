using UnityEngine;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class contains methods for the player to interact with the turret UI. 
/// Specifically this contains the behaviour for the player to select a turret.
/// </summary>

public class SelectTurretUI : MonoBehaviour {

    // MouseInput object communicates current user behaviour
    public MouseInput mouseInput;
    public PlayerStats playerStats;

    // Turret objects stored to be selected by the player 
    public GameObject turretPrefab1;
    public GameObject turretPrefab2;
    public GameObject moneyPrompt;

    public void SelectTurret1() {

        // Player must have enough money to select a turret
        if (playerStats.money > 200) {

            // Player may only select turret if they're not placing, or rotating one already
            if (!mouseInput.getRotatingTurret() &&
                mouseInput.getTurret() == null) {

                mouseInput.setTurret(Instantiate(turretPrefab1));
                mouseInput.InitTurretUnderMouse();

            }

        } else {

            // UI warning for not enough money
            moneyPrompt.SetActive(true);

        }

    }

    public void SelectTurret2() {

        // Player must have enough money to select a turret
        if (playerStats.money > 400) {

            // Player may only select turret if they're not placing, or rotating one already
            if (!mouseInput.getRotatingTurret() &&
                mouseInput.getTurret() == null) {

                mouseInput.setTurret(Instantiate(turretPrefab2));
                mouseInput.InitTurretUnderMouse();

            }

        } else {

            // UI warning for not enough money
            moneyPrompt.SetActive(true);

        }

    }
}
