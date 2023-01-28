using DG.Tweening;
using UnityEngine;

public class Car : MonoBehaviour, ICollision
{
  [SerializeField] private Vector3 _position;
  [SerializeField] private int _duration;
  [SerializeField] private Bang _bangTemplate;

  private Transform _startPoint;
  private Tween _tween;

  private void Start() => Move();

  private void Move()
  {
    _tween = transform
      .DOLocalMove(_position, _duration)
      .SetEase(Ease.Linear)
      .SetLoops(-1, LoopType.Restart);
  }

  private void Stop() => _tween.Kill();

  public void OnCollisionAction(Vector3 collisionObjectPosition)
  {
    Instantiate(_bangTemplate, collisionObjectPosition, Quaternion.identity);
    Stop();
  }
}