using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plant1 : MonoBehaviour
{
    public string NameScene = "Plant1_cactus_withflower";

    public void OnObjectClicked()
    {
        SceneManager.LoadScene(NameScene);
    }

}
