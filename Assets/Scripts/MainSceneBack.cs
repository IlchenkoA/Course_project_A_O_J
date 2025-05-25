using SaveLoadsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneBack : MonoBehaviour
{
    private GameObject dataBase;
    public void StartGameOnClick()
    {
        // Завантажує сцену з ім'ям "SampleScene"
        SceneManager.LoadScene("SampleScene");
    }

    // Метод, який викликається при натисканні на кнопку для переходу до кошика
    public void Cart()
    {
        // Завантажує сцену з ім'ям "Cart"
        SceneManager.LoadScene("Cart");
    }

    
    public void QuitGameOnClick()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
