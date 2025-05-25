using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    void Start()
    {
        
    }

    // Метод, що викликається один раз за кадр
    void Update()
    {
        // Перевіряємо, чи є активні дотики на екран
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Отримуємо позицію першого дотику на екрані
            Vector2 touchPosition = Input.GetTouch(0).position;

            // Створюємо луч, що виходить з головної камери через точку дотику
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            // Виконуємо Raycast і перевіряємо, чи торкнувся він якогось об'єкта
            if (Physics.Raycast(ray, out hit))
            {
                // Отримуємо об'єкт, з яким перетнувся луч
                GameObject obj = hit.collider.gameObject;

                // Перевіряємо, чи у об'єкта є компонент Donut1
                Donut1 objectInteraction = obj.GetComponent<Donut1>();
                if (objectInteraction != null)
                {
                    // Викликаємо метод OnObjectClicked() на об'єкті, якщо він існує
                    objectInteraction.OnObjectClicked();
                }
            }
        }
    }
}
