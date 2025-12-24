using System;
using UnityEngine;
using UnityEngine.UI;
using YG;
public class ChangeLangue : MonoBehaviour
{
   public static ChangeLangue instance;
   [SerializeField] private Image _flag;
   [SerializeField] private Sprite[] _flags;
   [SerializeField]private Slider _changelangue;
   public event Action ChangeLangueEvent;

   private void Awake()
   {
      _changelangue.value = PlayerPrefs.GetInt("ChangeLangue", 0);
      _changelangue.onValueChanged.AddListener((val) => ChangeLangueMy((int)val));
      ChangeLangueMy((int)_changelangue.value);
   }

   private void ChangeLangueMy(int value)
   {
      switch (value)
      {
         case 0:
            YG2.SwitchLanguage("ru");
            PlayerPrefs.SetInt("ChangeLangue", 0);
            _flag.sprite = _flags[value];
            break;
         case 1:
            YG2.SwitchLanguage("en");
            PlayerPrefs.SetInt("ChangeLangue", 1);
            _flag.sprite = _flags[value];
            break;
         default:
            
            break;
      }
      ChangeLangueEvent?.Invoke();
   }
}
