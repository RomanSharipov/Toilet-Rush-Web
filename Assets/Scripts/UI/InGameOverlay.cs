using Lean.Localization;
using TMPro;
using UnityEngine;

public class InGameOverlay : MonoBehaviour
{
  [SerializeField] private TMP_Text _text;
  [SerializeField] private UIHandler _ui;

  private int _index;

  private void OnEnable() => _ui.NextClicked += SetLevelText;

  private void OnDisable() => _ui.NextClicked -= SetLevelText;

  private void Start()
  {
    SetLevelText();
  }

  public void SetLevelText()
  {
    _index = LevelIndexSaver.LoadLevel() + 1;

    ChangeLevelIndexText();
  }

  public void ChangeLevelIndexText() =>
    _text.SetText(LeanLocalization.GetTranslationText("LevelToken") + " " + _index);
}