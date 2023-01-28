using Agava.YandexGames;
using Agava.YandexMetrica;
using UnityEngine;

public class AdvertisingInterstitial : MonoBehaviour
{
    [SerializeField] private Gamestopper _gamestopper;

    public void ShowAD()
    {
#if UNITY_EDITOR
        return;
#endif
        InterstitialAd.Show(OnOpenCallBack, OnCloseCallBack, OnErrorCallBack, OnOfflineCallBack);
    }

    private void OnOpenCallBack()
    {
        _gamestopper.Stop();
        _gamestopper.SwitchOffAllSounds();
        _gamestopper.BlockSwitchingSound();
        YandexMetrica.Send("InterstitialShow");
    }

    private void OnCloseCallBack(bool close)
    {
        Debug.Log($"OnCloseCallBack = {close}");
        _gamestopper.Play();
        _gamestopper.LoaderSoundVolumeValue.LoadValues();
        _gamestopper.UnBlockSwitchingSound();
    }

    private void OnErrorCallBack(string error)
    {
        Debug.Log($"OnErrorCallBack = {error}");
        _gamestopper.Play();
    }

    private void OnOfflineCallBack() => _gamestopper.Play();
}