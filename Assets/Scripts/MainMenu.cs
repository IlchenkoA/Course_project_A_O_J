using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameOnClick()
    {
        // Завантажує наступну сцену за індексом
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Метод, який викликається при натисканні на кнопку для виходу з гри
    public void QuitGameOnClick()
    {
        Debug.Log("GameQuit");

        // Закриває додаток
        Application.Quit();
    }
}
