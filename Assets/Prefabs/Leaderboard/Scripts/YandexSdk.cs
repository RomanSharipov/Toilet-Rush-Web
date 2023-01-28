using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexSdk : MonoBehaviour
{
  [SerializeField] private LocalizationStarter _localizationStarter;

  [SerializeField] private OverlayButton[] _buttons;

  private void Awake()
  {
    YandexGamesSdk.CallbackLogging = true;
  }

  private IEnumerator Start()
  {
#if UNITY_EDITOR
    _localizationStarter.Init();
    yield break;
#endif

    // Always wait for it if invoking something immediately in the first scene.
    yield return YandexGamesSdk.Initialize(OnSuccessCallback);

    while (true)
    {
      yield return new WaitForSecondsRealtime(0.25f);
    }
  }

  private void OnSuccessCallback()
  {
    Debug.Log("YandexSdk.cs OnSuccessCallback");
    _localizationStarter.Init();

    foreach (var button in _buttons)
      button.CheckPlatform();
  }
}