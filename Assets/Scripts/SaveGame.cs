using SaveLoadsSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class SaveGame : MonoBehaviour
{
    // Метод, який додає предмети до кошика та зберігає гру
    public void AddToCart()
    {
        // Виклик методу з класу SaveGameManager для збереження гри
        SaveGameManager.Save();
    }

    // Метод, який завантажує предмети з кошика 
    public void LoadToCart()
    {
        // Виклик методу з класу SaveGameManager для завантаження гри
        SaveGameManager.Load();
    }
}
