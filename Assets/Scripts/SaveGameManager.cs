using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

namespace SaveLoadsSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();


        public const string SaveDirectory = "/SaveGames/";
        public const string FileName = "SaveGame.sav";

  
        public static bool Save()
        {

            Debug.Log("Game Saved");

    
            var dir = Application.persistentDataPath + SaveDirectory;

      
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

         
            string json = JsonUtility.ToJson(CurrentSaveData, true);

     
            System.IO.File.WriteAllText(dir + FileName, json);

      
            GUIUtility.systemCopyBuffer = dir;

   
            return true;
        }

 
        public static void Load()
        {
        
            string fullPath = Application.persistentDataPath + SaveDirectory + FileName;

       
            SaveData tempData = new SaveData();

        
            if (System.IO.File.Exists(fullPath))
            {
                
                string json = System.IO.File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                
                Debug.LogError("Save file not found");
            }

        
            CurrentSaveData = tempData;
        }
    }
}