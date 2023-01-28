using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Unit _target;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private LayerMask _ignoreDrawing;
    [SerializeField] private AudioSource _roarSound;

    public event Action StartedMovement;
    public event Action StoppedMovement;

    private void Start() => StopMove();

    private void Update()
    {
        if (_target == null)
            return;

        // if (Physics.Linecast(transform.position, _target.transform.position, _ignoreDrawing))
            // return;

        transform.position = Vector3.MoveTowards(transform.position,
          new Vector3(_target.transform.position.x, _target.transform.position.y, transform.position.z),
          _speed * Time.deltaTime);
    }

    public void StartMove()
    {
        enabled = true;
        StartedMovement?.Invoke();
    }

    public void StopMove()
    {
        StoppedMovement?.Invoke();
        enabled = false;

        if (_target == null)
            return;
        _target.UnitMovement.IsReached -= StopMove;
    } 

    public void Init(Unit target)
    {
        _roarSound.Play();
        _target = target;
        _target.UnitMovement.IsReached += StopMove;
    }
}