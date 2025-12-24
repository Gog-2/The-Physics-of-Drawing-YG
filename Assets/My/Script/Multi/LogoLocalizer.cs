using UnityEngine;
using UnityEngine.UI;
using YG;

public class LogoLocalizer : MonoBehaviour
{
    [Header("Спрайты")]
    [SerializeField]private Sprite ruLogo; 
    [SerializeField]private Sprite enLogo;
    private ChangeLangue _changeLangue;

    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        ChangeLogo();
    }

    private void Start()
    {
        _changeLangue = ChangeLangue.instance;
        _changeLangue.ChangeLangueEvent += ChangeLogo;
    }

    void ChangeLogo()
    {
        if (YG2.lang == "ru")
        {
            imageComponent.sprite = ruLogo;
        }
        else
        {
            imageComponent.sprite = enLogo;
        }
    }
}