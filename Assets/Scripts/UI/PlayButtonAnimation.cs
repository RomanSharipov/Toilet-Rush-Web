using DG.Tweening;
using UnityEngine;

public class PlayButtonAnimation : MonoBehaviour
{
  [SerializeField] private Vector3 _offset;
  [SerializeField] private float _duration;

  private RectTransform _rectTransform;

  private void Start()
  {
    _rectTransform = GetComponent<RectTransform>();
    PlayAnimation();
  }

  private void PlayAnimation()
  {
    _rectTransform
      .DOScale(transform.localScale + _offset, _duration)
      .SetLoops(-1, LoopType.Yoyo)
      .SetEase(Ease.Linear);
  }
}