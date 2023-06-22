using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

}
