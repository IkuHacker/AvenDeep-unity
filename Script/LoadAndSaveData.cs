using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{

    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }
    void Start()
    {
        
        int currentHealth = PlayerPrefs.GetInt("playerHealth", PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.currentHealth = currentHealth;
        PlayerHealth.instance.healthBar.SetHealth(currentHealth);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("playerHealth", PlayerHealth.instance.currentHealth);
        PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock);
    }


}

    
