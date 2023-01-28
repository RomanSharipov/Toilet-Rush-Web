using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyondBarrierDrawMode : DrawLineMode
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _ignoreDrawing;

    private Vector3 _startPoint = new Vector3();
    private float _lastTime = 0;
    private float _delay = 0.2f;

    public event Action<Vector3> BarrierIsGone;

    public void Init(Vector3 startPoint)
    {
        _startPoint = startPoint;
    }

    private void Start()
    {
        _lastTime = _delay;
    }

    public override void Draw(Ray ray, out RaycastHit hit)
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 startPoint = new Vector3(_startPoint.x, _startPoint.y, PositionLineZ);
            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, PositionLineZ);

            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, hitPoint);

            if (Physics.Linecast(hitPoint, startPoint, out RaycastHit hitInfo, _ignoreDrawing))
                return;

            else
                BarrierIsGone?.Invoke(hitPoint);
            
        }
    }

}
