using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;

    private Camera _camera;
    private Vector2 _startPosition;
    private Vector2 _travel;

    private void Awake()
    {
        if (Camera.main != null)
        {
            _camera = Camera.main;
        }
        else
        {
            Debug.LogWarning("Main camera is not found. Parallax effect will not work properly.");
        }
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_camera == null) return;
        _travel = (Vector2)_camera.transform.position - _startPosition;
        transform.position = _startPosition + _travel * parallaxOffset;
    }
}