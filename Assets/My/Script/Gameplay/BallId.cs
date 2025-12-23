using System;
using TMPro;
using UnityEngine;

public class BallId : MonoBehaviour
{
    private int _id;
    [SerializeField] private TMP_Text _idText;
    private LvlLoader _lvlLoader;

    private void Awake()
    {
        _lvlLoader = LvlLoader.Instance;
        _lvlLoader.Reload += Destroy;
    }

    public int Id
    {
        get { return _id; }
        set
        {
            _id = value;
            _idText.text = _id.ToString();
        }
    }

    private void Destroy()
    {
        Destroy(this);
        _lvlLoader.Reload -= Destroy;
    }
}
