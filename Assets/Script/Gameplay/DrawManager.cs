using System.Collections;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _parentLine;
    
    public const float RESOLUTION = 0.1f;
    
    private Line _currentLine;
    void Start()
    {
        _cam = Camera.main;
    }
    void Update()
    {
        Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) MakeNewLine(mousePos); 

        if (Input.GetMouseButton(0)) PlacePoint(mousePos);
    }

    private void MakeNewLine(Vector3 mousePos)
    {
        if (_currentLine != null) _currentLine.CenterLineRenderer();
        _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
        TaskManager.Instance._lines.Add(_currentLine.gameObject);
    }

    private void PlacePoint(Vector3 mousePos)
    {
        _currentLine.SetPosition(mousePos);
    }
}
