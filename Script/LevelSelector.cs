
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    public Button[] levelButton;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButton.Length; i++)
        {
            if(i+1  > levelReached) 
            {
                levelButton[i].interactable = false;
            }
            
        }
    }
    public void LoadLevelPass(string levelName) 
    {
        SceneManager.LoadScene(levelName);
    }
}
