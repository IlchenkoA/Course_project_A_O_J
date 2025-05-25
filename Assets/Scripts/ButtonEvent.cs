using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    private GameObject database;

    // Поле для кнопки, яке буде налаштоване через інспектор Unity
    [SerializeField] private Button button;
    // Поле для продукту, яке буде налаштоване через інспектор Unity
    [SerializeField] private ProductAd productAd;

    private void Start()
    {
        // Знаходимо об'єкт з ім'ям "projectData" і зберігаємо посилання на нього
        database = GameObject.Find("projectData");

        // Додаємо слухача, який викликає метод при натисканні
        button.onClick.AddListener(buttonClick);
    }

    private void buttonClick()
    {
        // Отримуємо компонент PlayerSaveData з об'єкта database і додаємо продукт у кошик
        database.GetComponent<PlayerSaveData>().AddProductToCart(productAd);
    }

    // Метод, який викликається при знищенні скрипта
    private void OnDestroy()
    {
        // Видаляємо слухача подій з кнопки, щоб уникнути помилок
        button.onClick.RemoveListener(buttonClick);
    }
}
