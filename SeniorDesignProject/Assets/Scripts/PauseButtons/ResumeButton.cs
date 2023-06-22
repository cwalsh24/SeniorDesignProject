using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void UnpauseGame()
    {
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
    }

}
