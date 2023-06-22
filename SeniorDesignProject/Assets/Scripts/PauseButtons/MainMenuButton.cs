using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    public void MainMenuClick()
    {
        Time.timeScale = 1f;

        pauseMenuUI.SetActive(false);

        SceneManager.LoadScene("MainMenu");

        Destroy(GameObject.Find("MainCamera"));
        Destroy(GameObject.Find("Hero(Clone)"));
        Destroy(GameObject.Find("UI Canvas"));
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
