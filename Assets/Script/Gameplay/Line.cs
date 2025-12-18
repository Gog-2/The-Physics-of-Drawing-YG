using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _layerMask;
    private readonly List<Vector2> _points = new List<Vector2>();
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;
        Collider2D hit = Physics2D.OverlapPoint(pos, _layerMask);
        if (hit != null) return;
        if (_points.Count > 0)
        {
            RaycastHit2D lineHit = Physics2D.Linecast(_points[_points.Count - 1], pos, _layerMask);
            if (lineHit.collider != null)
            {
                Debug.Log("Линия пересекает запретную зону!");
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
        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public void ActivatePhysic()
    {
        CenterLineRenderer();
        _rb.isKinematic = false;
    }
    public void CenterLineRenderer()
    {
        Vector3[] positions = new Vector3[_renderer.positionCount];
        _renderer.GetPositions(positions);
        
        Vector3 center = Vector3.zero;
        for (int i = 0; i < positions.Length; i++)
        {
            center += positions[i];
        }
        center /= positions.Length;
        
        _renderer.useWorldSpace = false;
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] -= center;
            _renderer.SetPosition(i, positions[i]);
        }
        
        _rb.centerOfMass = Vector3.zero;
    }
}
