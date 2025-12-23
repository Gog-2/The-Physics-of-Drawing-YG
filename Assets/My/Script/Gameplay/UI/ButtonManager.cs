using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefs = RedefineYG.PlayerPrefs;
using YG;

public class ButtonManager : MonoBehaviour
{
    [Header("Ad Settings")]
    [Tooltip("Показывать рекламу каждые N уровней")]
    [Range(3, 6)]
    public int levelsBeforeAd = 3;
    
    private const string LEVELS_COUNTER_KEY = "LevelsBeforeAdCounter";

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
        CheckAndShowAd();
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
        int levelsCounter = PlayerPrefs.GetInt(LEVELS_COUNTER_KEY, 0);
        levelsCounter++;
        PlayerPrefs.SetInt(LEVELS_COUNTER_KEY, levelsCounter);
    
        PlayerPrefs.Save();
    }
    
    private void CheckAndShowAd()
    {
        int levelsCounter = PlayerPrefs.GetInt(LEVELS_COUNTER_KEY, 0);

        if (levelsCounter >= levelsBeforeAd)
        {
            PlayerPrefs.SetInt(LEVELS_COUNTER_KEY, 0);
            PlayerPrefs.Save();
            YG2.InterstitialAdvShow();
            Debug.Log($"Реклама вызвана после {levelsBeforeAd} уровней");
        }
    }
}
