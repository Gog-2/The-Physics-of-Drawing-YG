using System;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class AdsAndLvlOpen : MonoBehaviour
{
    static public AdsAndLvlOpen instance;
    private int _counterLvl = 0;
    private int _openLvl = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _openLvl = PlayerPrefs.GetInt("Level", 0);
    }
}
