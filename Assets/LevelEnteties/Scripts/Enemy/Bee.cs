using DG.Tweening;
using UnityEngine;

public class Bee : MonoBehaviour
{
  [SerializeField] private Transform _endPoint;
  [SerializeField] private GameObject _model;
  [SerializeField] private float _duration;

  private void Start() => LoopMove();

  private void LoopMove()
  {
    _model.transform
      .DOMove(_endPoint.position, _duration)
      .SetLoops(-1, LoopType.Yoyo)
      .SetEase(Ease.Linear);
  }
}