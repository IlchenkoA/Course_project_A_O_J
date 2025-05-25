using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    private GameObject database;

    // ���� ��� ������, ��� ���� ����������� ����� ��������� Unity
    [SerializeField] private Button button;
    // ���� ��� ��������, ��� ���� ����������� ����� ��������� Unity
    [SerializeField] private ProductAd productAd;

    private void Start()
    {
        // ��������� ��'��� � ��'�� "projectData" � �������� ��������� �� �����
        database = GameObject.Find("projectData");

        // ������ �������, ���� ������� ����� ��� ���������
        button.onClick.AddListener(buttonClick);
    }

    private void buttonClick()
    {
        // �������� ��������� PlayerSaveData � ��'���� database � ������ ������� � �����
        database.GetComponent<PlayerSaveData>().AddProductToCart(productAd);
    }

    // �����, ���� ����������� ��� ������� �������
    private void OnDestroy()
    {
        // ��������� ������� ���� � ������, ��� �������� �������
        button.onClick.RemoveListener(buttonClick);
    }
}
