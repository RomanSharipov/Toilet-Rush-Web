using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gamestopper : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private LoaderSoundVolumeValue _loaderSoundVolumeValue;

    private bool _isPaused = false;
    private bool _switchingSoundIsBlocking = false;

    public LoaderSoundVolumeValue LoaderSoundVolumeValue => _loaderSoundVolumeValue;

    private void Start()
    {
        _loaderSoundVolumeValue.LoadValues();
    }

    public void Stop()
    {
        Time.timeScale = 0;
        _isPaused = true;
    }

    public void Play()
    {
        Time.timeScale = 1;
        _isPaused = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (_switchingSoundIsBlocking)
            return;

        if (focus)
        {
            _loaderSoundVolumeValue.LoadValues();
        }

        else
        {
            SwitchOffAllSounds();
        }

        if (_isPaused)
            return;

        if (focus)
        {
            Time.timeScale = 1;
        }

        else
        {
            Time.timeScale = 0;
        }
    }

    public void SwitchOffAllSounds()
    {
        AudioListener.pause = true;
        //_audioMixerGroup.audioMixer.SetFloat(MixerConstants.Music, -80.0f);
        //_audioMixerGroup.audioMixer.SetFloat(MixerConstants.Effects, -80.0f);
    }

    public void BlockSwitchingSound()
    {
        _switchingSoundIsBlocking = true;
    }

    public void UnBlockSwitchingSound()
    {
        _switchingSoundIsBlocking = false;
    }


}
