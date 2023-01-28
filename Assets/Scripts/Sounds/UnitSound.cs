using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSound : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private UnitCollisionHandler _unitCollisionHandler;
    [SerializeField] private AudioSource _walkAudio;

    private void OnEnable()
    {
        _unit.StartedMovement += SwitchOnWalkSound;
        _unit.StoppedMovement += SwitchOffWalkSound;
    }

    private void OnDisable()
    {
        _unit.StartedMovement -= SwitchOnWalkSound;
        _unit.StoppedMovement -= SwitchOffWalkSound;
    }

    private void SwitchOnWalkSound()
    {
        _walkAudio.Play();
    }

    private void SwitchOffWalkSound()
    {
        _walkAudio.Stop();
    }
}
