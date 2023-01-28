using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionsPanel : MonoBehaviour
{
  [SerializeField] private Button _crossButton;

  private CanvasGroup _canvasGroup;
  private Vector3 _offset = new Vector3(0.1f, 0.1f, 0.1f);
  private float _duration = .3f;

  private void OnEnable()
  {
    _canvasGroup = GetComponent<CanvasGroup>();
    _crossButton.onClick.AddListener(ClosePanel);
  }

  private void OnDisable() => _crossButton.onClick.RemoveListener(ClosePanel);

  private void ClosePanel()
  {
    _canvasGroup.interactable = false;
    transform.DOScale(Vector3.zero, _duration);
  }

  public void ShowPanel()
  {
    Sequence _sequence = DOTween.Sequence();

    _canvasGroup.interactable = true;

    _sequence
      .Append(transform.DOScale(Vector3.one + _offset, _duration))
      .Append(transform.DOScale(Vector3.one, 0.2f));
  }
}