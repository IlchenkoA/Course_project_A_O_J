using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Donut1 : MonoBehaviour
{
    public string NameScene = "Donut_one";

    public void OnObjectClicked()
    {
        // ��� �� ��������� �������, ��� ������� ������� �� ���� �����
        SceneManager.LoadScene(NameScene);
    }

}
