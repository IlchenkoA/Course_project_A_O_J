using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace SaveLoadsSystem
{
    // Атрибут [System.Serializable] дозволяє клас перетворювати в формат, який може бути збережений і відновлений
    [System.Serializable]
    public class SaveData
    {
        public CollectibleState PlayerData = new CollectibleState();
    }
}