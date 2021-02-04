using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Jack Collins
/// 
/// This is called when either the player or player's 
/// point to defend has 0 health. Which activates 
/// the game over scene.
/// 
/// </summary>

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    // Function used to end the game when the player runs out of health.
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Cursor.lockState = CursorLockMode.Confined;
            GameOver();
        }
    }

    void GameOver()
    {
        // Loads the game over screen
        SceneManager.LoadScene("GameOver");
    }
}
