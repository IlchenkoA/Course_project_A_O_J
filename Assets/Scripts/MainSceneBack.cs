using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneBack : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Cart()
    {
        SceneManager.LoadScene("Cart");
    }

    public void BackToLastVisitedFlower()
    {
        string lastFlowerScene = PlayerPrefs.GetString("LastVisitedFlowerScene", "SampleScene");
        SceneManager.LoadScene(lastFlowerScene);
    }


    public void QuitGameOnClick()
    {
        Debug.Log("GameQuit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
