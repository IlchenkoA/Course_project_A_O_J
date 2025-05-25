using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

namespace SaveLoadsSystem
{
    // Статичний клас, який керує збереженням та завантаженням гри
    public static class SaveGameManager
    {
        // Поточні дані гри
        public static SaveData CurrentSaveData = new SaveData();

        // Константи для шляху та назви файлу збереження
        public const string SaveDirectory = "/SaveGames/";
        public const string FileName = "SaveGame.sav";

        // Метод для збереження гри
        public static bool Save()
        {
            // Вивід повідомлення про збереження гри у консоль
            Debug.Log("Game Saved");

            // Формування шляху до директорії збереження
            var dir = Application.persistentDataPath + SaveDirectory;

            // Перевірка існування директорії, якщо не існує, то створюється нова
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            // Перетворення поточних даних гри в формат JSON
            string json = JsonUtility.ToJson(CurrentSaveData, true);

            // Запис JSON у файл
            System.IO.File.WriteAllText(dir + FileName, json);

            // Копіювання шляху збереження у буфер обміну
            GUIUtility.systemCopyBuffer = dir;

            // Повертаємо значення true, щоб підтвердити успішне збереження
            return true;
        }

        // Метод для завантаження гри
        public static void Load()
        {
            // Формування повного шляху до файлу збереження
            string fullPath = Application.persistentDataPath + SaveDirectory + FileName;

            // Тимчасові дані для збереження зчитаних даних з файлу
            SaveData tempData = new SaveData();

            // Перевірка існування файлу збереження
            if (System.IO.File.Exists(fullPath))
            {
                // Зчитування JSON з файлу та конвертування його у структуру SaveData
                string json = System.IO.File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                // Вивід помилки, якщо файл збереження не знайдено
                Debug.LogError("Save file not found");
            }

            // Оновлення поточних даних гри
            CurrentSaveData = tempData;
        }
    }
}