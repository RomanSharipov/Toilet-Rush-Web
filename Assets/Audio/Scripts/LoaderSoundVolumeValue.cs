using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoaderSoundVolumeValue : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [Header("Music handlers")]
    [SerializeField] private SwitchManager _musicSwitcher;
    [SerializeField] private SliderManager _musicSlider;
    [Header("Sounds handlers")]
    [SerializeField] private SwitchManager _soundsSwitcher;
    [SerializeField] private SliderManager _soundsSlider;

    public void LoadValues()
    {
        LoadMusicValues();
        LoadSoundsValues();
        AudioListener.pause = false;
    }

    private void LoadMusicValues()
    {
        if (_musicSwitcher.isOn)
        {
            float currentVolume = (_musicSlider.mainSlider.value * 80.0f) - 80.0f;

            _audioMixerGroup.audioMixer.SetFloat(MixerConstants.Music, currentVolume);
        }
        else
        {
            _audioMixerGroup.audioMixer.SetFloat(MixerConstants.Music, -80.0f);
        }
    }

    private void LoadSoundsValues()
    {
        if (_soundsSwitcher.isOn)
        {
            float currentVolume = (_soundsSlider.mainSlider.value * 80.0f) - 80.0f;

            _audioMixerGroup.audioMixer.SetFloat(MixerConstants.Effects, currentVolume);
        }

        else
        {
            _audioMixerGroup.audioMixer.SetFloat(MixerConstants.Effects, -80.0f);
        }
    }
}
