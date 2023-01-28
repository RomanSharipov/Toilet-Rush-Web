using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector3[] _points;
    private List<Vector3> _pointsLIst = new List<Vector3>();
    private Tween _movementAnimation;
    private int _index = 0;
    private float _speed = 2.0f;
    private float _minDistance = 0.01f;
    private float _currentDistance;
    private Line _linePath;

    public event Action IsReached;
    public event Action IsStopped;

    public void PathComplete()
    {
        IsReached?.Invoke();
        IsStopped?.Invoke();
    } 

    public void InitPath(Line linePath, ToiletEditor toiletEditor)
    {
        _linePath = linePath;

        for (int i = 0; i < _linePath.MovementPoints.Count; i++)
            _pointsLIst.Add(_linePath.MovementPoints[i]);

        _pointsLIst.Add(toiletEditor.transform.position);
    }

    public void StartMove()
    {
        _movementAnimation = transform
          .DOPath(_pointsLIst.ToArray(), 2.0f)
          .SetEase(Ease.Linear)
          .OnComplete(() => PathComplete());
    }

    public void StopMove() => _movementAnimation.Kill();
}