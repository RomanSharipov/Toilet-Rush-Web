using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLines : MonoBehaviour
{
    [SerializeField] Line _lineTemplate;

    private Camera _camera;
    private Line _currentDrawingLine;

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    public Line StartDrawNewLine(MouseInput mouseInput,Color colorLine)
    {
        _currentDrawingLine = Instantiate(_lineTemplate,transform);
        _currentDrawingLine.Init(_camera, mouseInput, colorLine);
        return _currentDrawingLine;
    }

    public void StopDrawLine()
    {
        if (_currentDrawingLine != null)
        {
            _currentDrawingLine.enabled = false;
        }
    }

    public void DestroyAllLines()
    {
        Line[] lines = GetComponentsInChildren<Line>();

        for (int i = 0; i < lines.Length; i++)
        {
            Destroy(lines[i].gameObject);
        }
    }
}
