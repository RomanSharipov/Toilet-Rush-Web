using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const string Toilet = "Toilet";

    [SerializeField] private UnitMovement _unitMovement;
    [SerializeField] private Color _colorLine;
    [SerializeField] private string _gender;
    [SerializeField] private UnitCollisionHandler _unitCollisionHandler;
    [SerializeField] private AudioSource _confettiSound;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ParticleSystem _confetti;

    private Line _linePath;
    private ToiletEditor _currentToilet;

    public bool PathAlreadyExist => _linePath != null;
    public bool IsBeyondBarrierDrawMode => _linePath.IsBeyondBarrierDrawMode;
    public Color ColorLine => _colorLine;
    public string Gender => _gender;
    public UnitMovement UnitMovement => _unitMovement;

    public event Action WasCollision;
    public event Action StartedMovement;
    public event Action StoppedMovement;
    public event Action ArrivedOnToilet;
    public event Action ArrivedOnCabine;
    

    public void SetLine(Line linePath) => _linePath = linePath;

    private void OnEnable()
    {
        _unitCollisionHandler.WasCollised += OnCollised;
        _unitMovement.IsStopped += StopMove;
    }

    private void OnDisable()
    {
        _unitCollisionHandler.WasCollised -= OnCollised;
        _unitMovement.IsStopped -= StopMove;
    } 

    public void SetGender(Gender gender, Color colorLine)
    {
        _gender = gender.ToString();
        _colorLine = colorLine;
    }

    public void ResetLine()
    {
        if (_linePath != null)
        {
            Destroy(_linePath.gameObject);
        }
        _linePath = null;
    }

    public void InitMovementPath(ToiletEditor targetpoint)
    {
        _currentToilet = targetpoint;

        if (targetpoint == null)
            throw new NullReferenceException(nameof(targetpoint));

        _unitMovement.InitPath(_linePath, targetpoint);
    }

    public void StartMove()
    {
        _unitMovement.StartMove();
        StartedMovement?.Invoke();
    }

    public void StopMove()
    {
        _unitMovement.StopMove();
        StoppedMovement?.Invoke();
    }

    private void OnCollised()
    {
        _renderer.enabled = false;
        WasCollision?.Invoke();
        StopMove();
    }

    public void OnPathComplete()
    {
        StopMove();
        if (_currentToilet.ActualTypeToilet == Toilet)
        {
            ArrivedOnToilet?.Invoke();
        }
        else
        {
            ArrivedOnCabine?.Invoke();
        }
        _confetti.Play();
        _confettiSound.Play();
    }
}