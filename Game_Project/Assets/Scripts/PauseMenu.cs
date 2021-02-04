using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Jack Collins
/// 
/// This class contains the behaviour for the in-game pause menu.
/// This contains a listener for the escape key to pause the game state
/// and resume it at the click of a button.
/// </summary>

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public EnemyManager enemyManager;
    public GameObject pauseMenuUI;
    public GameObject playerObject;
    public Canvas crosshair;

    // Checks if the ecape key has been pressed
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                // If the game was already paused it will resume
                Resume();
                crosshair.enabled = true;
                playerObject.GetComponent<playerAim>().enabled = true;
                playerObject.GetComponent<Shoot>().enabled = true;
                playerObject.GetComponent<animationController>().enabled = true;
                playerObject.GetComponent<characterMovement>().enabled = true;
                playerObject.GetComponent<PlayerStats>().enabled = true;
            } else {
                // Pauses the game
                Pause();
                Cursor.lockState = CursorLockMode.Confined;
                crosshair.enabled = false;
                playerObject.GetComponent<playerAim>().enabled = false;
                playerObject.GetComponent<Shoot>().enabled = false;
                playerObject.GetComponent<animationController>().enabled = false;
                playerObject.GetComponent<characterMovement>().enabled = false;
                playerObject.GetComponent<PlayerStats>().enabled = false;
            }
        }   
    }

    public void Resume() {
        if (enemyManager.getCombat() == true) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.Confined;
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        crosshair.enabled = true;
        playerObject.GetComponent<playerAim>().enabled = true;
        playerObject.GetComponent<Shoot>().enabled = true;
        playerObject.GetComponent<animationController>().enabled = true;
        playerObject.GetComponent<characterMovement>().enabled = true;
        playerObject.GetComponent<PlayerStats>().enabled = true;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
        playerObject.GetComponent<playerAim>().enabled = true;
        playerObject.GetComponent<Shoot>().enabled = true;
        playerObject.GetComponent<animationController>().enabled = true;
        playerObject.GetComponent<characterMovement>().enabled = true;
        playerObject.GetComponent<PlayerStats>().enabled = true;
        Time.timeScale = 1f;
    }

    // Ends the application completely
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        playerObject.GetComponent<playerAim>().enabled = true;
        playerObject.GetComponent<Shoot>().enabled = true;
        playerObject.GetComponent<animationController>().enabled = true;
        playerObject.GetComponent<characterMovement>().enabled = true;
        playerObject.GetComponent<PlayerStats>().enabled = true;
        Time.timeScale = 1f;
    }
}
