using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ResetDataButton : MonoBehaviour
{
   public void ResetData()
   {
      PlayerPrefs.SetInt("ActiveLvl", 0);
      PlayerPrefs.SetInt("Level", 1);
      PlayerPrefs.Save();
      SceneManager.LoadScene(0);
   }
}
