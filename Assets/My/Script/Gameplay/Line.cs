using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _layerMask;
    private readonly List<Vector2> _points = new List<Vector2>();
    
    [Header("Optimization")]
    [SerializeField] private float _simplifyTolerance = 0.15f;
    private TaskManager _taskManager;
    private LvlLoader _lvlLoader;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer.useWorldSpace = false;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        _collider.edgeRadius = 0.05f;
        _taskManager = TaskManager.Instance;
        _taskManager.StartGameEvent += ActivatePhysic;
        _lvlLoader = LvlLoader.Instance;
        _lvlLoader.Reload += DestroySelf;
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;
        Collider2D hit = Physics2D.OverlapPoint(pos, _layerMask);
        if (hit != null) 
        {
            Debug.Log($"Попал в запретную зону: {hit.gameObject.name}");
            return;
        }
    
        if (_points.Count > 0)
        {
            Vector2 lastWorldPos = transform.TransformPoint(_points[_points.Count - 1]);
            RaycastHit2D lineHit = Physics2D.Linecast(lastWorldPos, pos, _layerMask);
            if (lineHit.collider != null)
            {
                Debug.Log($"Линия пересекает: {lineHit.collider.gameObject.name}");
                return;
            }
        }
    
        Vector2 localPos = transform.InverseTransformPoint(pos);
        _points.Add(localPos);
        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, localPos);
    
        _collider.points = _points.ToArray();
    }


    private bool CanAppend(Vector2 pos)
    {
        if (_renderer.positionCount == 0) return true;
        Vector3 lastWorldPos = transform.TransformPoint(_renderer.GetPosition(_renderer.positionCount - 1));
        return Vector2.Distance(lastWorldPos, pos) > DrawManager.RESOLUTION;
    }
    private void ActivatePhysic()
    {
        SimplifyLine(); 
        CenterLineRenderer(); 
        _rb.bodyType = RigidbodyType2D.Dynamic; 
        _rb.gravityScale = 1f;
    }
    private void SimplifyLine()
    {
        if (_renderer.positionCount < 3) return;
        
        Vector3[] originalPoints = new Vector3[_renderer.positionCount];
        _renderer.GetPositions(originalPoints);
        List<Vector3> simplifiedPoints = new List<Vector3>();
        LineUtility.Simplify(
            new List<Vector3>(originalPoints), 
            _simplifyTolerance, 
            simplifiedPoints
        );
        
        Debug.Log($"Упрощение: {originalPoints.Length} → {simplifiedPoints.Count} точек (-{100 - simplifiedPoints.Count * 100 / originalPoints.Length}%)");
        
        _renderer.positionCount = simplifiedPoints.Count;
        _renderer.SetPositions(simplifiedPoints.ToArray());
        
        _points.Clear();
        foreach (Vector3 point in simplifiedPoints)
        {
            _points.Add(new Vector2(point.x, point.y));
        }
    }
    public void CenterLineRenderer()
    {
        if (_renderer.positionCount < 2) return;
        
        Vector3[] positions = new Vector3[_renderer.positionCount];
        _renderer.GetPositions(positions);
        
        Vector3 localCenter = Vector3.zero;
        for (int i = 0; i < positions.Length; i++)
        {
            localCenter += positions[i];
        }
        localCenter /= positions.Length; 
        
        Vector3 worldCenter = transform.TransformPoint(localCenter);
        transform.position = worldCenter;

        _points.Clear();
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 newLocalPos = positions[i] - localCenter;
            _renderer.SetPosition(i, newLocalPos);
            _points.Add(new Vector2(newLocalPos.x, newLocalPos.y));
        }
        
        _collider.enabled = false;
        _collider.points = _points.ToArray();
        _collider.enabled = true;
        _rb.centerOfMass = Vector2.zero;
    }

    private void DestroySelf()
    {
        _taskManager.StartGameEvent -= ActivatePhysic;
        Destroy(gameObject);
        _lvlLoader.Reload -= DestroySelf;
    }
}
