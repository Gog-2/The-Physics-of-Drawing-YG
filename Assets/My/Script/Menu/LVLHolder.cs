using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class LVLHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _locker;
    private int _lvl = 0;

    public void Init(int lvl,bool locked)
    {
        _text.text = lvl.ToString();
        _locker.SetActive(locked);
        _lvl = lvl;
    }
    

    public void LoadScene()
    {
        PlayerPrefs.SetInt("ActiveLvl", _lvl);
        SceneManager.LoadScene(1);
    }
}
