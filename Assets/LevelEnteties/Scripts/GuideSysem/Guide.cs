using DG.Tweening;
using UnityEngine;

public class Guide : MonoBehaviour
{
  [SerializeField] private GuideTrail _guideTrail;
  [SerializeField] private Color[] _colors;
  [SerializeField] private ColorType _color;

  private PathPool _pathpool;
  private Transform[] _path;
  private Vector3[] _pathVector3;

  private void Start()
  {
    _pathpool = GetComponentInChildren<PathPool>();
    _path = _pathpool.GetComponentsInChildren<Transform>();

    SetColor();
    Sort();
    MoveOnPath();
  }

  private void Sort()
  {
    _pathVector3 = new Vector3[_path.Length];

    for (int i = 0; i < _path.Length; i++)
      _pathVector3[i] = _path[i].position;
  }

  private void SetColor()
  {
    if (_color == ColorType.red)
      _guideTrail.SetColor(_colors[0]);
    if (_color == ColorType.blue)
      _guideTrail.SetColor(_colors[1]);
    if (_color == ColorType.yellow)
      _guideTrail.SetColor(_colors[2]);
    if (_color == ColorType.green)
      _guideTrail.SetColor(_colors[3]);
  }

  private void MoveOnPath()
  {
    _guideTrail.transform
      .DOPath(_pathVector3, .5f * _path.Length, PathType.CatmullRom, PathMode.TopDown2D)
      .OnStepComplete(_guideTrail.Restart)
      .SetLoops(-1, LoopType.Restart)
      .SetEase(Ease.Linear);
  }
}