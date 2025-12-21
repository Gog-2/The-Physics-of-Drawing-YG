using UnityEngine;
using UnityEngine.EventSystems; 

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _parentLine;
    private bool _gameStarted = false;
    private TaskManager _taskManager;
    
    public const float RESOLUTION = 0.1f;
    
    private Line _currentLine;
    
    void Start()
    { 
        _taskManager = TaskManager.Instance;
        _taskManager.StartGameEvent += GameStarted;   
        _cam = Camera.main;
    }
    
    void Update()
    {
        if (!_gameStarted)
        {
            Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUI())
                {
                    Debug.Log("Клик по UI - рисование заблокировано");
                    return;
                }

                MakeNewLine(mousePos2D);
            }

            if (Input.GetMouseButton(0) && _currentLine != null)
            {
                if (!IsPointerOverUI())
                {
                    PlacePoint(mousePos2D);
                }
            }
        }
    }
    
    private bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return true;
        }
        
        return false;
    }

    private void MakeNewLine(Vector2 mousePos)
    {
        Collider2D hit = Physics2D.OverlapPoint(mousePos, _layerMask);
        if (hit != null) 
        {
            Debug.Log("Нельзя начать рисование в запретной зоне!");
            return;
        }
        
        if (_currentLine != null) _currentLine.CenterLineRenderer();
        _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        TaskManager.Instance._lines.Add(_currentLine);
    }

    private void PlacePoint(Vector2 mousePos)
    {
        _currentLine.SetPosition(mousePos);
    }
    private void GameStarted() => _gameStarted = true;
}
