using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _enemyMovement.StartedMovement += Run;
        _enemyMovement.StoppedMovement += Idle;
    }

    private void OnDisable()
    {
        _enemyMovement.StartedMovement -= Run;
        _enemyMovement.StoppedMovement -= Idle;
    }

    private void Run()
    {
        _animator.SetBool(AnimatorConstants.Run,true);
    }

    private void Idle()
    {
        _animator.SetBool(AnimatorConstants.Run, false);
    }
}
