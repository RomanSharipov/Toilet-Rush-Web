using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using Agava.YandexGames;

public class LocalizationStarter : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private string _language = "tr";
    [SerializeField] private InGameOverlay _inGameOverlay;

    public void Init()
    {
#if UNITY_EDITOR
        _leanLocalization.SetCurrentLanguage(_language);
        return;
#endif

        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "tr":
                _language = "Turkish";
                break;
            case "en":
                _language = "English";
                break;
            case "ru":
                _language = "Russian";
                break;
        }
        _leanLocalization.SetCurrentLanguage(_language);
        _inGameOverlay.ChangeLevelIndexText();
    }
}
