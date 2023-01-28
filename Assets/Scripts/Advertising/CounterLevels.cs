using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterLevels : MonoBehaviour
{
    [SerializeField] private RewardAdvertising _rewardAdvertising;
    [SerializeField] private AdvertisingInterstitial _advertisingInterstitial;
    [SerializeField] private int _currentLevelIndex;
    [SerializeField] private UIHandler _uIHandler;
    [SerializeField] private CompleteLevelPanel _completeLevelPanel;
    [SerializeField] private NewSkinPanel _newSkinPanel;

    private int _frequencyShowingAD = 1;
    private int _frequencyShowingNewSkin = 4;

    private void OnEnable()
    {
        _uIHandler.NextClicked += TryShowAD;
        _completeLevelPanel.OpenedCompletePanel += TryShowNewSkin;
    }

    private void OnDisable()
    {
        _uIHandler.NextClicked -= TryShowAD;
        _completeLevelPanel.OpenedCompletePanel -= TryShowNewSkin;
    } 

    private void TryShowAD()
    {
        _currentLevelIndex = ES3.Load(LevelIndexSaver.Level, 0);

        if (_rewardAdvertising.InterstitialAdIsBlocking)
            return;

        if (_currentLevelIndex % _frequencyShowingAD == 0)
        {
            _advertisingInterstitial.ShowAD();
        }

    }

    private void TryShowNewSkin()
    {
        _currentLevelIndex = ES3.Load(LevelIndexSaver.Level, 0);

        if (_rewardAdvertising.InterstitialAdIsBlocking)
            return;

        _currentLevelIndex++;

        if (_currentLevelIndex % _frequencyShowingNewSkin == 0)
        {
            _newSkinPanel.TryOpen();
        }

    }
}
