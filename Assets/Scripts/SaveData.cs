using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace SaveLoadsSystem
{
   
    [System.Serializable]
    public class SaveData
    {
        public CollectibleState PlayerData = new CollectibleState();
    }
}