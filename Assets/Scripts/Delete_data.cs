using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Delete_data : MonoBehaviour
{
    public GameObject name;

    // поле для зберігання контейнера
    public GameObject conteiner;

    private void Start()
    {
        
    }

    // Публічний метод для видалення продукту з кошика
    public void dell()
    {
        // Викликає метод RemoveProductFromCart з класу PlayerSaveData, передаючи назву продукту для видалення
        PlayerSaveData.RemoveProductFromCart(name.GetComponent<TextMeshProUGUI>().text);

        // Знищує поточний об'єкт
        Destroy(this.gameObject);
    }
    private void LateUpdate()
    {
        // Знаходить об'єкт з ім'ям "Container" і зберігає посилання на нього в змінну conteiner
        conteiner = GameObject.Find("Container");

        // Встановлює поточний об'єкт дочірнім для контейнера
        this.gameObject.transform.SetParent(conteiner.transform);
    }
}
