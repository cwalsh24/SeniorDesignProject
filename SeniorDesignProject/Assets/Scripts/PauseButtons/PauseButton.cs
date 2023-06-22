using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void PauseGame()
    {

        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
    }
}
