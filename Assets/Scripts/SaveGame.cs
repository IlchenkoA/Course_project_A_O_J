using SaveLoadsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class SaveGame : MonoBehaviour
{
    // �����, ���� ���� �������� �� ������ �� ������ ���
    public void AddToCart()
    {
        // ������ ������ � ����� SaveGameManager ��� ���������� ���
        SaveGameManager.Save();
    }

    // �����, ���� ��������� �������� � ������ 
    public void LoadToCart()
    {
        // ������ ������ � ����� SaveGameManager ��� ������������ ���
        SaveGameManager.Load();
    }
}
