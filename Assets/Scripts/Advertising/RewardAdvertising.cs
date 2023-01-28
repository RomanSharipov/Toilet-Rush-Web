using Agava.YandexGames;
using System;
using UnityEngine;

public class RewardAdvertising : MonoBehaviour
{
  [SerializeField] private Gamestopper _gamestopper;

  private bool _interstitialAdIsBlocking = false;

  public bool InterstitialAdIsBlocking => _interstitialAdIsBlocking;

  public void TryShowAD(Action onReward)
  {
#if UNITY_EDITOR
    onReward.Invoke();
    return;
#endif
    VideoAd.Show(OnOpen, onReward, OnClose, OnError);
  }

  private void OnReward()
  {
  }

  private void OnOpen()
  {
    _interstitialAdIsBlocking = true;
    _gamestopper.Stop();
    _gamestopper.SwitchOffAllSounds();
    _gamestopper.BlockSwitchingSound();
  }

  private void OnClose()
  {
    _gamestopper.Play();
    _gamestopper.LoaderSoundVolumeValue.LoadValues();
    _gamestopper.UnBlockSwitchingSound();
    _interstitialAdIsBlocking = false;
  }

  private void OnError(string error)
  {
    Debug.Log($"VideoAd.Show() Error = {error}");
  }
}