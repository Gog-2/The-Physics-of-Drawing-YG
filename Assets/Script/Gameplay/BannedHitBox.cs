using UnityEngine;

public class BannedHitBox : MonoBehaviour
{
    [SerializeField] private LayerMask _forbiddenLayer;
    [SerializeField] private Camera _camera;
    
    private bool _isInForbiddenZone = false;

    void Start()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    void Update()
    {
        CheckMousePosition();
    }

    private void CheckMousePosition()
    {
        Vector2 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, _forbiddenLayer);
        
        if (hit != null)
        {
            if (!_isInForbiddenZone)
            {
                _isInForbiddenZone = true;
                Debug.Log("Мышь вошла в запретную зону!");
                OnEnterForbiddenZone();
            }
        }
        else
        {
            if (_isInForbiddenZone)
            {
                _isInForbiddenZone = false;
                Debug.Log("Мышь покинула запретную зону");
                OnExitForbiddenZone();
            }
        }
    }

    private void OnEnterForbiddenZone()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnExitForbiddenZone()
    {
    
    }

    public bool IsMouseInForbiddenZone()
    {
        return _isInForbiddenZone;
    }


}
