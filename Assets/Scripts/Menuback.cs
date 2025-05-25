using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuback : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGameOnClick()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
