using System;
using TMPro;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private int _id;
    private BallToBasketManager _ballToBasketManager;
    private int _counterValue = 0;
    [SerializeField] private TMP_Text _idText;
    private LvlLoader _lvlLoader;

    private int _counter
    {
        get
        {
            return _counterValue;
        }
        set
        {
            _counterValue = value;
            if(_ballToBasketManager == null) return;
            UpdateStatus();
        }
    }

    public void Intz(BallToBasketManager manager, int id)
    {
        _ballToBasketManager = manager;
        _id = id;
        _idText.text = _id.ToString();
    }

    private void UpdateStatus() => _ballToBasketManager.UpdateValueInBucket(_counter);
    private void OnTriggerEnter2D(Collider2D other)
    {
        BallId chekId = other.gameObject.GetComponent<BallId>();
        if (chekId == null) return;
        if (chekId.Id == _id)
        {
            _counter++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BallId chekId = other.gameObject.GetComponent<BallId>();
        if (chekId == null) return;
        if (chekId.Id == _id)
        {
            _counter--;
        }
    }
}
