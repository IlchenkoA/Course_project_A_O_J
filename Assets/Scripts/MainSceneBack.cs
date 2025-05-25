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
        // ��������� ����� � ��'�� "SampleScene"
        SceneManager.LoadScene("SampleScene");
    }

    // �����, ���� ����������� ��� ��������� �� ������ ��� �������� �� ������
    public void Cart()
    {
        // ��������� ����� � ��'�� "Cart"
        SceneManager.LoadScene("Cart");
    }

    
    public void QuitGameOnClick()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }
}
