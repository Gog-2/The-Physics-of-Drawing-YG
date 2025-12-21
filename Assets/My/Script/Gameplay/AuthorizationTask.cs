using System;
using System.Collections.Generic;
using UnityEngine;

public class AuthorizationTask : MonoBehaviour
{
    static public AuthorizationTask instance;
    [SerializeField]private List<bool> _tasks =  new List<bool>();
    [SerializeField] private GameObject _endGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetKey()
    {
      bool key = false;
      _tasks.Add(key);
      return _tasks.Count - 1;
    }

    public void Authorization(int key,bool status)
    {
        _tasks[key] = status;
        CheckAllTask();
    }

    private void CheckAllTask()
    {
        bool finish = true;
        foreach (bool task in _tasks)
        {
            if (!task) finish = false;
        }
        if (finish) Win();
    }

    private void Win()
    {
        _endGame.SetActive(true);
    }
}
