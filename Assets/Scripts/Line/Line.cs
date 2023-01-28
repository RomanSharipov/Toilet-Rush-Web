using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private DrawLineMode _currentDrawMode;
    [SerializeField] private DefaultDrawMode _defaultDrawMode;
    [SerializeField] private BeyondBarrierDrawMode _beyondBarrierDrawMode;

    private Camera _camera;
    private MouseInput _mouseInput;
    private Vector3 _previousPointMousePosition = new Vector3();

    public List<Vector3> MovementPoints => _defaultDrawMode.PointsForMovement;
    public bool IsBeyondBarrierDrawMode => _currentDrawMode is BeyondBarrierDrawMode;

    private void OnEnable()
    {
        _beyondBarrierDrawMode.BarrierIsGone += SetDefaultDrawMode;
        _defaultDrawMode.BarrierHasAppeared += SetBeyondBarrierDrawMode;
    }

    private void OnDisable()
    {
        _beyondBarrierDrawMode.BarrierIsGone -= SetDefaultDrawMode;
        _defaultDrawMode.BarrierHasAppeared -= SetBeyondBarrierDrawMode;
    }

    public void Init(Camera camera, MouseInput mouseInput,Color colorLine)
    {
        _camera = camera;
        _mouseInput = mouseInput;
        _defaultDrawMode.Init(colorLine);
        SetStartPositionPreviousPoint();
    }

    private void Draw(Vector3 mousePosition)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        _currentDrawMode.Draw(ray, out hit);
    }

    private void FixedUpdate()
    {
        Draw(_mouseInput.MousePosition);
    }

    private void SetDefaultDrawMode(Vector3 mousePosition)
    {
        _beyondBarrierDrawMode.gameObject.SetActive(false);
        _currentDrawMode = _defaultDrawMode;
    }

    private void SetBeyondBarrierDrawMode(Vector3 endPointOnLine)
    {
        _beyondBarrierDrawMode.gameObject.SetActive(true);

        if (_defaultDrawMode.Points.Count == 0)
            return;

        _beyondBarrierDrawMode.Init(endPointOnLine);
        _currentDrawMode = _beyondBarrierDrawMode;
    }

    private void SetStartPositionPreviousPoint()
    {
        Ray ray = _camera.ScreenPointToRay(_mouseInput.MousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            _previousPointMousePosition = hit.point;
        }
    }
}
