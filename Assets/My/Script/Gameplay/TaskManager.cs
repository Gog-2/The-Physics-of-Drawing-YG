using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TaskManager : MonoBehaviour
{
    static public TaskManager Instance;
    public List<Line> _lines = new List<Line>();
    [SerializeField] private int _maxLvlLine = 3;
    [SerializeField] private List<TextHolder> _textTask = new List<TextHolder>();
    public event Action StartGameEvent;

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

    public void ClearLine()
    {
        for (int i = _maxLvlLine; i < _lines.Count; i--)
        {
            Destroy(_lines[i]);
        }
    }
    public void StartGame()
    {
        StartGameEvent?.Invoke();
    }
}
