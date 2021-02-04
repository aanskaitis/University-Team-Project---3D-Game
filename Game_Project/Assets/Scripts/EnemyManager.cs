using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Lee Taylor
/// 
/// This class contains enemy management. Per wave, enemies are enabled one by one,
/// this keeps track of the number of enemies alive and switches from combat mode to 
/// build mode accordingly. 
/// </summary>

public class EnemyManager : MonoBehaviour {

    [Header("Game Information")]
    private GameObject[] allWaves;

    [Header("Wave Information")]
    public int currentAmountAlive; // Tracks number of enemies alive
    public GameObject turretMode; // Used to switch to turret mode 
    public float enemyInterval = 2f; // Time in between each enemy is enabled
    public ValidAreas validAreas; // Stores all valid areas for turrets
    public Text currentWaveText; // Displays wave number to user 
    public float enemySpeedIncrease = 0.15f; // Enemy speed increases each wave by this amount
    private int wavePointer = 0; // Points to which wave is currently active
    private bool combat = true; // Indicates whether the player is in combat or not
    private bool pressedStart = false; // Indicates whether the player wishes to start the next wave

    [Header("Objects")]
    public GameObject playerObject; // Disables the player when in build mode
    public GameObject overHeadCam; // Ref. to the camera over the level
    public GameObject objectManager; // Indirect ref. to the green zones visibility depending on truth value of combat
    public Canvas crosshair; // Ref. to the crosshair visible depending on truth value of combat
    private CinemachineVirtualCamera overHeadVirtualCam; // Used to get overheadcontroller component
    private OverheadController overheadController; // This moves the overhead camera and is disabled when combat is True

    // Each waveX object has children objects that are enemies
    // each child enemy object is enabled one by one during combat
    [Header("Waves")]
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    public GameObject wave4;
    public GameObject wave5;

    // private int wavePointer = 0;
    private int enemyPointer;
    private float timeSinceLastSpawn;  // Enemy spawn interval

    private void countEnemies() {
        // Sum the number of enemies in a wave

        foreach (Transform path in ((GameObject)allWaves.GetValue(wavePointer)).transform) {
            currentAmountAlive += path.transform.childCount;
        }

    }

    void Start() {

        // Stores wave objects in, allWaves array
        allWaves = new GameObject[5];
        allWaves.SetValue(wave1, 0);
        allWaves.SetValue(wave2, 1);
        allWaves.SetValue(wave3, 2);
        allWaves.SetValue(wave4, 3);
        allWaves.SetValue(wave5, 4);

        // Level starts off with combat
        turretMode.SetActive(false);

        // Update enemyPointer, enemy counter, and time since last spawn
        updateEnemyPointer();
        countEnemies();
        timeSinceLastSpawn = Time.time;

        // Disable the component to move the overhead camera 
        overHeadVirtualCam = (CinemachineVirtualCamera)overHeadCam.GetComponent("CinemachineVirtualCamera");
        overheadController = overHeadCam.GetComponent<OverheadController>();
        overheadController.enabled = false;

    }

    private void updateEnemyPointer() {
        // Sets enemy pointer to the last enemy
        if (wavePointer <= 4) {
            enemyPointer = ((GameObject)allWaves.GetValue(wavePointer)).transform.GetChild(0).transform.childCount - 1;
        } else {
            checkWin();
            return;
        }
    }

    public void updateWaveText() {
        // Updates wave text displayed to the user
        currentWaveText.text = (wavePointer + 1).ToString();
    }

    public bool getCombat() {
        return combat;
    }

    public void setCombat(bool state) {
        combat = state;
    }

    public void setPressedStart(bool state) {
        pressedStart = state;
    }

    public void decreaseCurrentAlive() {
        currentAmountAlive--;
    }

    private void enableEnemy() {

        // Exit the method if the pointer, points out of range
        if (enemyPointer < 0) {
            return;
        }

        // Checks if enough time has elapsed to enable the next enemy
        if ((Time.time - timeSinceLastSpawn) >= enemyInterval) {

            // Enables each enemy at each different point/path in the map one by one using the enemy pointer
            foreach (Transform path in ((GameObject)allWaves.GetValue(wavePointer)).transform) {
                GameObject enemy = path.transform.GetChild(enemyPointer).gameObject;
                enemy.GetComponent<enemyMovement>().speed += enemySpeedIncrease;
                enemy.SetActive(true);
            }

            enemyPointer--;
            timeSinceLastSpawn = Time.time;

        }

    }

    private void checkWin() {
        if (wavePointer == 5) {
            SceneManager.LoadScene("GameWin");
            return;
        }
    }

    private void validateMode() {
        // Checks the amount of enemies alive to determine 
        // if the game state should be switched to build mode

        if (currentAmountAlive == 0) {

            checkWin();

            // Disable player object, enable turret mode, enable the cursor
            playerObject.SetActive(false);
            turretMode.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;

            // Set green zones visible, set combat to false, increase wave pointer, 
            objectManager.GetComponent<ValidAreas>().setValidZones(true);
            combat = false;
            wavePointer++;

            // Increase enemy interval to spawn, set player pressed start to false 
            enemyInterval += 0.2f;
            pressedStart = false;

            // Disable the crosshair, and enable the overhead camera controller
            crosshair.enabled = false;
            overheadController.enabled = true;
        }

    }

    void Update() {

        // If player is in combat, activate the next enemy and
        // check if the player should not be in combat mode
        if (combat == true) {

            enableEnemy();
            validateMode();

            // If player is not in combat, check if the player has started
            // the next combat round if so...
        }
        else if (pressedStart) {
            /* ... View is switched from overhead to over shoulder, turret placing mode is disabled,
             * green zones become invisible, lock the mouse pointer/cursor, combat and cross hair is true */
            overheadController.enabled = false;
            turretMode.SetActive(false);
            validAreas.setValidZones(false);
            Cursor.lockState = CursorLockMode.Locked;
            combat = true;
            crosshair.enabled = true;

            // Wave number displayed to user is updated
            updateWaveText();

            // Enemypointer and number of enemies alive is updated
            updateEnemyPointer();
            countEnemies();

        }

    }
}
