using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPause = false;
    public GameObject pauseMenu;
    public static PauseMenu instance;


    private void Awake()
    {

        
        if (instance != null)
        {

            return;
        }

        instance = this;


    }


    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetButtonDown("Pause") && !DialogTrigger.instance.IsDialogStart) 
        {
            if (gameIsPause) 
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }


    public void Pause() 
    {
        PlayerMovement.instance.enabled = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPause = true;

    }

    public void Resume()
    {
        PlayerMovement.instance.enabled = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPause = false;

    }


    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
        

    }
}
