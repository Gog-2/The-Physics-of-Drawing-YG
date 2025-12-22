using TMPro;
using UnityEngine;

public class BallToBasketManager : MonoBehaviour
{
    public bool MadedTask = false;
    [SerializeField] private BallBox _box;
    [SerializeField] private BoxSideOpen _sideOpen;
    [SerializeField] private int Ammount = 1;
    [SerializeField] private Bucket _bucket;
    public int id;
    private int _inBucket = 0;
    [SerializeField] private TextHolder _textHolderPrefab;
    [SerializeField]private GameObject _textHolderParent;
    private TextHolder _textHolder;
    [SerializeField] private string _textHolderText;
    private int _keyAutorization;
    

    private void Awake()
    {
        _box.Intz(Ammount,_sideOpen,id);
        _bucket.Intz(this,id);
        _textHolder = Instantiate(_textHolderPrefab, _textHolderParent.transform);
    }

    private void Start()
    {
        _keyAutorization = AuthorizationTask.instance.GetKey();
        _textHolder.Text($"{id}. {_textHolderText} {Ammount}/{_inBucket}");
    }
    private void UpdateTextHolder() => _textHolder.Text(_textHolderText + $"{Ammount}/{_inBucket}");
    
    public void UpdateValueInBucket(int value)
    {
        _inBucket = value;
        if (value == Ammount)
        {
            MadedTask = true;
            _textHolder.ChangeColor(Color.green);
            AuthorizationTask.instance.Authorization(_keyAutorization,true);
        }
        else
        {
            MadedTask = false;
            _textHolder.ChangeColor(Color.white);
            AuthorizationTask.instance.Authorization(_keyAutorization,false);
        }

        UpdateTextHolder();
    }
}
