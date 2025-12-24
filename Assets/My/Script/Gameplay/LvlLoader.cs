using System;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class LvlLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] Lvls;
    [SerializeField] private Transform _parentLvl;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _activeLvl;
    [SerializeField] private GameObject _winPanel;
    private int _currentLvl;
    public event Action Reload;
    static public LvlLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadLvl();
    }

    public void Clear()
    {
        Destroy(_activeLvl);
        Reload?.Invoke();
    } 

    public void LoadLvl()
    {
        _currentLvl = PlayerPrefs.GetInt("ActiveLvl", 1) - 1;
        if (_currentLvl < 0 || _currentLvl >= Lvls.Length)
        {
            Debug.LogError($"Уровень {_currentLvl} не существует! Всего уровней: {Lvls.Length}");
            PlayerPrefs.SetInt("ActiveLvl",1);
            _currentLvl = 0;
        }
        _activeLvl = Instantiate(Lvls[_currentLvl], _parentLvl.position, Quaternion.identity);   
        if (_currentLvl == 0)
        {
            _tutorial.SetActive(true);
        }
        _winPanel.SetActive(false);
    }
        
}
