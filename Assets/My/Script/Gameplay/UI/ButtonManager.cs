using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMainMenuBeatedLvl()
    {
        AdsAndLvlOpen.instance.LvlBeated(SceneManager.GetActiveScene().buildIndex);
        GoToMainMenu();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AdsAndLvlOpen.instance.LvlBeated(SceneManager.GetActiveScene().buildIndex);
    }
}
