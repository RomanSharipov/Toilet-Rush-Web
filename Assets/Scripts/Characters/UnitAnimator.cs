using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Unit _unit;

    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _unit.StartedMovement += Run;
        _unit.ArrivedOnToilet += SitOnToilet;
        _unit.ArrivedOnCabine += OnArrivedOnCabine;
    }

    private void OnDisable()
    {
        _unit.StartedMovement -= Run;
        _unit.ArrivedOnToilet -= SitOnToilet;
        _unit.ArrivedOnCabine -= OnArrivedOnCabine;
    }

    private void Run()
    {
        _animator.SetBool(AnimatorConstants.Run, true);
    }

    private void SitOnToilet()
    {
        _animator.SetBool(AnimatorConstants.SitOnToilet, true);
    }

    private void OnArrivedOnCabine()
    {
        _animator.SetBool(AnimatorConstants.ArrivedOnCabine, true);
    }
}
