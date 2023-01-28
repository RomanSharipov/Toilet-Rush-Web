using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SwitcherSoundSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private string _audioMixerName;
    [SerializeField] private SliderManager _switchManager;

    private void OnEnable()
    {
        _switchManager.onValueChanged.AddListener(SwitchSounds);
    }

    private void OnDisable()
    {
        _switchManager.onValueChanged.RemoveListener(SwitchSounds);
    }

    public void SwitchSounds(float value)
    {
        float totalValue = (value * 80.0f) - 80.0f;
        
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerName, totalValue);
    }
}
