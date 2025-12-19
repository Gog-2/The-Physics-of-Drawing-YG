using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextHolder : MonoBehaviour
{
    [SerializeField]private TMP_Text _text;
    [SerializeField]private Image _image;
    public void Text(string text) => _text.text = text;
    public void ChangeColor(Color color) => _image.color = color;
}
