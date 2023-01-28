using DG.Tweening;
using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenu : MonoBehaviour
{
  [SerializeField] private LevelHandler _levelHandler;
  [SerializeField] private Button _playButton;
  [SerializeField] private float _fadeDuration = .2f;
  [SerializeField] private TMP_Text _levelCounter;

  private CanvasGroup _canvasGroup;
  private int _index;

  private void Start() => SetCounter();

  private void OnEnable()
  {
    _canvasGroup = GetComponent<CanvasGroup>();
    _playButton.onClick.AddListener(Hide);
  }

  private void OnDisable() => _playButton.onClick.RemoveListener(Hide);

  public void SetCounter()
  {
    _index = LevelIndexSaver.LoadLevel() + 1;

    _levelCounter.text =
      (LeanLocalization.GetTranslationText("LevelToken") + " " + _index + "/70");
  }

  private void Hide()
  {
    _canvasGroup.DOFade(0, _fadeDuration);
    _levelHandler.SpawnCurrentLevel();
    gameObject.SetActive(false);
  }

  public void Show()
  {
    gameObject.SetActive(true);
    _canvasGroup.DOFade(1, _fadeDuration);
  }
}