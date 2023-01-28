using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private SpawnerLines _spawnerLines;
    [SerializeField] private Camera _camera;
    [SerializeField] private Unit _selectedUnit;
    [SerializeField] private List<Unit> _units = new ();
    [SerializeField] private List<Enemy> _enemies = new ();

    private void Start() => _spawnerLines.Init(_camera);

    private void OnEnable()
    {
        _mouseInput.WasKeyDown += OnKeyDown;
        _mouseInput.WasKeyUp += OnKeyUp;
    }

    private void OnDisable()
    {
        _mouseInput.WasKeyDown -= OnKeyDown;
        _mouseInput.WasKeyUp -= OnKeyUp;
    }

    public void SetUnitsList(List<Unit> units) => _units = units;
    public void SetEnemiesList(List<Enemy> enemies) => _enemies = enemies;

    public void DestroyAllLines()
    {
        _spawnerLines.DestroyAllLines();
    }

    private void OnKeyDown()
    {
        if (TryGetGameobjectUnderCursor(out Unit character))
        {
            if (character.PathAlreadyExist)
                return;

            _selectedUnit = character;
            Line newLine = _spawnerLines.StartDrawNewLine(_mouseInput, character.ColorLine);
            _selectedUnit.SetLine(newLine);
        }
    }

    private void OnKeyUp()
    {
        if (_selectedUnit != null)
        {
            if (_selectedUnit.PathAlreadyExist)
            {
                if (_selectedUnit.IsBeyondBarrierDrawMode)
                {
                    _selectedUnit.ResetLine();
                    return;
                }
            }
        }

        if (TryGetGameobjectUnderCursor(out ToiletEditor toilet))
        {
            if (_selectedUnit != null)
            {
                if (toilet.ActualGender == ToiletGender.Universal.ToString() || toilet.ActualGender == _selectedUnit.Gender)
                {
                    _spawnerLines.StopDrawLine();
                    _selectedUnit?.InitMovementPath(toilet);

                    if (CheckReadyPathAllCharacters())
                        StartMovingAllCharacters();
                }

                else
                    _selectedUnit?.ResetLine();
            }
        }
        else
            _selectedUnit?.ResetLine();

        _selectedUnit = null;
    }

    private bool CheckReadyPathAllCharacters()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].PathAlreadyExist)
                continue;

            else
                return false;
        }

        return true;
    }

    private void StartMovingAllCharacters()
    {
        foreach (var character in _units)
        {
            character.StartMove();
        }

        foreach (var enemy in _enemies)
        {
            enemy.StartMove();
        }
    }

    private bool TryGetGameobjectUnderCursor<T>(out T newGameobject) where T : MonoBehaviour
    {
        Ray ray = _camera.ScreenPointToRay(_mouseInput.MousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.TryGetComponent(out T gameobject))
            {
                newGameobject = gameobject;
                return true;
            }

            newGameobject = null;
            return false;
        }

        newGameobject = null;
        return false;
    }
}