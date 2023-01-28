using System;
using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
  private Collider _collider;
  private SpriteRenderer _renderer;
  private Vector3 _targetRotation = new Vector3(0, 0, 90);
  private float _duration = .2f;

  public event Action _isTouch;

  private void Awake()
  {
    _collider = GetComponent<BoxCollider>();
    _renderer = GetComponent<SpriteRenderer>();
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (collider.TryGetComponent(out Unit unit))
    {
      _collider.enabled = false;
      ChangeLevelRotation();
      _isTouch?.Invoke();
    }
  }

  private void ChangeLevelRotation() => transform.DORotate(_targetRotation,_duration).SetEase(Ease.Linear);

  public void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
}