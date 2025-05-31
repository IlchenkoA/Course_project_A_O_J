using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGameOnClick()
    {
        Debug.Log("GameQuit");

#if UNITY_EDITOR
        // Зупиняє гру в редакторі Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // Закриває гру в збірці
    Application.Quit();
#endif
    }
}
