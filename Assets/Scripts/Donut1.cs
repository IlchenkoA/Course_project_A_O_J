using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Donut1 : MonoBehaviour
{
    public string NameScene = "Donut_one";

    public void OnObjectClicked()
    {
        // Тут ми викликаємо функцію, яка запускає перехід на нову сцену
        SceneManager.LoadScene(NameScene);
    }

}
