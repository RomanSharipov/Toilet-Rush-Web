using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Unit> _units = new ();
    [SerializeField] private List<Enemy> _enemies = new ();
    private List<Guide> _guides = new ();
    private int _reachedIndex = 0;
    private bool _eventAlreadyIsHappened;
    [SerializeField] private bool _guidesEnabledOnStart = false;

    public List<Unit> Units => _units;
    public List<Enemy> Enemies => _enemies;

    public event Action LevelComplete;
    public event Action LevelFail;

    private void OnEnable()
    {
        _eventAlreadyIsHappened = false;

        var guides = GetComponentsInChildren<Guide>();
        var units = GetComponentsInChildren<Unit>();
        var enemies = GetComponentsInChildren<Enemy>();

        foreach (var guide in guides)
            _guides.Add(guide);

        foreach (var unit in units)
            _units.Add(unit);

        foreach (var unit in _units)
        {
            unit.UnitMovement.IsReached += AddReachedUnit;
            unit.WasCollision += LoseLevel;
        }

        foreach (var enemy in enemies)
            _enemies.Add(enemy);

        if (_guidesEnabledOnStart)
            return;

        foreach (var guide in _guides)
            guide.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (var unit in _units)
        {
            unit.UnitMovement.IsReached -= AddReachedUnit;
            unit.WasCollision -= LoseLevel;
        }
    }

    public void ShowGuides()
    {
        foreach (var guide in _guides)
            guide.gameObject.SetActive(true);
    }

    private void AddReachedUnit()
    {
        _reachedIndex++;

        if (_reachedIndex == _units.Count)
        {
            foreach (var unit in _units)
            {
                unit.OnPathComplete();
            }

            foreach (var enemy in _enemies)
            {
                enemy.StopMove();
            }

            if (_eventAlreadyIsHappened)
                return;

            LevelComplete?.Invoke();
            _eventAlreadyIsHappened = true;
        }
    }

    private void LoseLevel()
    {
        foreach (var unit in _units)
        {
            unit.StopMove();
        }

        foreach (var enemy in _enemies)
        {
            enemy.StopMove();
        }


        if (_eventAlreadyIsHappened)
            return;

        LevelFail?.Invoke();
        _eventAlreadyIsHappened = true;
    }
}