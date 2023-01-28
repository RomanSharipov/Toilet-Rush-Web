using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDrawMode : DrawLineMode
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _ignoreDrawing;

    private List<Vector3> _points = new List<Vector3>();
    private Path _path;

    public List<Vector3> Points => _points;
    public List<Vector3> PointsForMovement => _path.Points;

    public event Action<Vector3> BarrierHasAppeared;

    public override void Draw(Ray ray, out RaycastHit hit)
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            TryAddPointToDrawLine(hit.point);
            _path.TryAddPointToMovementPath(hit.point);
        }
    }

    public void Init(Color colorLine)
    {
        _lineRenderer.material.color = colorLine;
        _path = new Path(PositionLineZ);
    }

    private void TryAddPointToDrawLine(Vector3 newPoint)
    {
        newPoint.z = PositionLineZ;

        Vector3 startPoint = GetTotalNewStartPoint(newPoint);
        
        startPoint.z = PositionLineZ;
        if (Physics.Linecast(startPoint, newPoint, out RaycastHit hitInfo, _ignoreDrawing))
        {
            BarrierHasAppeared?.Invoke(startPoint);
            return;
        }

        _points.Add(newPoint);
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }

    private Vector3 GetTotalNewStartPoint(Vector3 newPoint)
    {
        Vector3 startPoint = new Vector3();

        newPoint.z = PositionLineZ;

        if (_points.Count == 0)
            startPoint = newPoint;

        else
            startPoint = _points[_points.Count - 1];

        startPoint.z = PositionLineZ;
        return startPoint;
    }
}
