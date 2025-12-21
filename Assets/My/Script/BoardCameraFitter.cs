using UnityEngine;

public class BoardCameraFitter : MonoBehaviour
{
    public Camera cam;
    public Transform boardRoot; 

    void Start()
    {
        FitToBoard();
    }

    void FitToBoard()
    {
        Bounds boardBounds = GetChildrenBounds(boardRoot);
        float boardWidth  = boardBounds.size.x;
        float boardHeight = boardBounds.size.y;
        float aspect = cam.aspect; 
        float sizeByWidth  = boardWidth  / (2f * aspect);
        float sizeByHeight = boardHeight / 2f;

        cam.orthographic = true;
        cam.orthographicSize = Mathf.Max(sizeByWidth, sizeByHeight);

        cam.transform.position = new Vector3(
            boardBounds.center.x,
            boardBounds.center.y,
            cam.transform.position.z
        );
    }

    Bounds GetChildrenBounds(Transform root)
    {
        var renderers = root.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
            return new Bounds(root.position, Vector3.zero);

        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            b.Encapsulate(renderers[i].bounds);
        }

        return b;
    }
}