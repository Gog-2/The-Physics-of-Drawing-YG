using TMPro;
using UnityEngine;

public class BallId : MonoBehaviour
{
    private int _id;
    [SerializeField] private TMP_Text _idText;
    public int Id
    {
        get { return _id; }
        set
        {
            _id = value;
            _idText.text = _id.ToString();
        }
    }
}
