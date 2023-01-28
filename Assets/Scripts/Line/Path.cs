using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path 
{
    private Vector3 _previousFramePointForMove = new Vector3();
    private List<Vector3> _pointsForMovement = new List<Vector3>();
    private float _minDistanceBetweenPointForMove = 0.7f;
    private float _currentDistanceBetweenPointsForMove;
    private float _positionLineZ;

    public List<Vector3> Points => _pointsForMovement;

    public Path(float positionLineZ)
    {
        _positionLineZ = positionLineZ;
    }

    public void TryAddPointToMovementPath(Vector3 newPoint)
    {
        _currentDistanceBetweenPointsForMove = Vector3.Distance(newPoint, _previousFramePointForMove);

        if (_currentDistanceBetweenPointsForMove > _minDistanceBetweenPointForMove)
        {
            newPoint.z = _positionLineZ;
            _pointsForMovement.Add(newPoint);
            _previousFramePointForMove = new Vector3(newPoint.x, newPoint.y, -0.2f);
        }
    }
}
