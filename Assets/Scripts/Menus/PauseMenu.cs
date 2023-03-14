using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    //UI elements
    public GameObject pauseMenuUI;
    public GameObject dialogueCanvasUI;

    public bool gameIsPaused;

    public GameObject arrow;

    public Transform resumeTransform;
    public Transform quitTransform;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        instance.pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (gameIsPaused)
        {
            MenuUsage();
        }
    }

    public void MenuUsage()
    {
        // Arrow movement
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // On resume button, up to quit
            if (arrow.transform.position == resumeTransform.position)
            {
                arrow.transform.position = quitTransform.position;
            }
            // On quit button, up to resume
            else if (arrow.transform.position == quitTransform.position)
            {
                arrow.transform.position = resumeTransform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // On resume button, down to quit
            if (arrow.transform.position == resumeTransform.position)
            {
                arrow.transform.position = quitTransform.position;
            }
            // On quit button, down to resume
            else if (arrow.transform.position == quitTransform.position)
            {
                arrow.transform.position = resumeTransform.position;
            }
        }

        //Selection
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Resume Button Select
            if (arrow.transform.position == resumeTransform.position)
            {
                Resume();
            }

            //Quit Button Select
            else if (arrow.transform.position == quitTransform.position)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
        }
        //Maybe put back in later
        /*
        //De-selection
        if (Input.GetKeyDown(KeyCode.X))
        {
            Resume();
        }
        */
    }

    void Resume()
    {
        instance.pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        dialogueCanvasUI.SetActive(true);
    }

    void Pause()
    {
        instance.pauseMenuUI.SetActive(true);
        dialogueCanvasUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
