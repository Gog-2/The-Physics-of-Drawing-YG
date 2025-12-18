using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    static public TaskManager Instance;
    public List<GameObject> _lines = new List<GameObject>();
    [SerializeField] private int _maxLvlLine = 3;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ClearEsxes()
    {
        for (int i = _maxLvlLine; i < _lines.Count; i--)
        {
            Destroy(_lines[i]);
        }
    }
    public void StartGame()
    {
        
    }
}
