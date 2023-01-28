using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SwitcherSound : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private string _audioMixerName; 
    [SerializeField] private SwitchManager _switchManager;
    [SerializeField] private SliderManager _sliderManager;

    private void OnEnable()
    {
        if (_switchManager.isOn == false)
        {
            _sliderManager.gameObject.SetActive(false);
        }

        _switchManager.OffEvents.AddListener(OnDisableSwitchManager);
        _switchManager.OnEvents.AddListener(OnEnableSwitchManager);
    }

    private void OnDisable()
    {
        _switchManager.OffEvents.RemoveListener(OnDisableSwitchManager);
        _switchManager.OnEvents.RemoveListener(OnEnableSwitchManager);
    }

    private void OnDisableSwitchManager()
    {
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerName, -80.0f);

        _sliderManager.gameObject.SetActive(false);
    }

    private void OnEnableSwitchManager()
    {
        float currentVolume = (_sliderManager.mainSlider.value * 80.0f) - 80.0f;

        _audioMixerGroup.audioMixer.SetFloat(_audioMixerName, currentVolume);

        _sliderManager.gameObject.SetActive(true);
    }
}
