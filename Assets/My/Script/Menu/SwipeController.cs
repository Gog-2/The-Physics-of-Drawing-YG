using UnityEngine;
using UnityEngine.EventSystems;
public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPages;
    private int _currentPage;
    private Vector3 _targetPosition;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform levelPagesRect;
    [SerializeField] private float tweenTime = 0.5f;
    [SerializeField] private LeanTweenType tweenType = LeanTweenType.linear;
    private float _dragThreshold = 0.2f;

    private void Awake()
    {
        _currentPage = 1;
        _targetPosition = levelPagesRect.localPosition;
        _dragThreshold = Screen.width / 15;
    }
    public void NextPage()
    {
        if (_currentPage < maxPages)
        {
            _currentPage++;
            _targetPosition += pageStep;
            MovePage();
        }
    }

    public void PreviousPage()
    {
        if (_currentPage > 1)
        {
            _currentPage--;
            _targetPosition -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(_targetPosition, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > _dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x) PreviousPage();
            else NextPage();
        }
        else
        {
            MovePage();
        }
    }
    
}
