using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ButtonManager : MonoBehaviour
{
    public void ReloadScene()
    {
        LvlLoader.Instance.Clear();
        LvlLoader.Instance.LoadLvl();
    }

    public void GoToMainMenu()
    {
        LvlLoader.Instance.Clear();
        SceneManager.LoadScene(0);
    }

    public void GoToMainMenuBeatedLvl()
    {
        SaveStats();
        GoToMainMenu();
    }

    public void NextLevel()
    {
        LvlLoader.Instance.Clear();
        SaveStats();
        LvlLoader.Instance.LoadLvl();
    }

    private void SaveStats()
    {
        int currentLvl = PlayerPrefs.GetInt("ActiveLvl", 0);
        int newActiveLvl = currentLvl + 1;
        PlayerPrefs.SetInt("ActiveLvl", newActiveLvl);
        
        int savedLvl = PlayerPrefs.GetInt("Level", 0);
        if (newActiveLvl > savedLvl)
        {
            PlayerPrefs.SetInt("Level", newActiveLvl);
        }
    
        PlayerPrefs.Save();
    }
}
