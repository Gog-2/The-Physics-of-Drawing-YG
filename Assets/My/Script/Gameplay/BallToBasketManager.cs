using TMPro;
using UnityEngine;

public class BallToBasketManager : MonoBehaviour
{
    public bool MadedTask = false;
    [SerializeField] private BallBox _box;
    [SerializeField] private BoxSideOpen _sideOpen;
    [SerializeField] private int Ammount = 1;
    [SerializeField] private Bucket _bucket;
    private int _id;
    private int _inBucket = 0;
    [SerializeField] private TextHolder _textHolderPrefab;
    [SerializeField]private GameObject _textHolderParent;
    private TextHolder _textHolder;
    [SerializeField] private string _textHolderText;
    private int _keyAutorization;
    private LvlLoader _lvlLoader;
    private bool _changelvl = false;

    private void Start()
    {
        _textHolderParent = TaskConnect.instance.gameObject;
        _textHolder = Instantiate(_textHolderPrefab, _textHolderParent.transform);
        _keyAutorization = AuthorizationTask.instance.GetKey();
        _id =  _keyAutorization;
        _textHolder.Text($"{_id}. {_textHolderText} {Ammount}/{_inBucket}");
        _box.Intz(Ammount,_sideOpen,_id);
        _bucket.Intz(this,_id);
        _lvlLoader = LvlLoader.Instance;
        _lvlLoader.Reload += Destroy;
    }
    private void UpdateTextHolder() => _textHolder.Text(_textHolderText + $"{Ammount}/{_inBucket}");
    
    public void UpdateValueInBucket(int value)
    {
        if (_changelvl) return;
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

    private void Destroy()
    {
        _changelvl = true;
        Destroy(_textHolder.gameObject);
        Destroy(gameObject);
        _lvlLoader.Reload -= Destroy;
    }
}
