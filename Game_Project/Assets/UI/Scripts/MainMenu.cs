using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour{

    // Changes the scene to the first level
    public void Level1 ()
    {
        SceneManager.LoadScene("LevelOne");
    }

    // Changes the scene to the second level
    public void Level2()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    // Changes the scene to the third level
    public void Level3()
    {
        SceneManager.LoadScene("LevelThree");
    }

    // Quits the application
    public void QuitGame ()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
