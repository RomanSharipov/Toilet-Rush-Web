using UnityEngine;

public class Gates : MonoBehaviour
{
  [SerializeField] private ColorType _color;
  [SerializeField] private Color[] _colors;
  [SerializeField] private Sprite[] _sprites;
  [SerializeField] private Lever _lever;

  private Gate _gate;

  private void OnEnable()
  {
    _lever = GetComponentInChildren<Lever>();
    _gate = GetComponentInChildren<Gate>();
    _lever._isTouch += OpenGates;
  }

  private void OnDisable() => _lever._isTouch -= OpenGates;

  private void Start() => SetColors();

  private void SetColors()
  {
    if (_color == ColorType.blue)
    {
      _lever.SetSprite(_sprites[0]);
      _gate.SetColor(_colors[0]);
    }
    else
    {
      _lever.SetSprite(_sprites[1]);
      _gate.SetColor(_colors[1]);
    }
  }

  private void OpenGates() => _gate.Open();
}

enum ColorType
{
  red = 0,
  blue,
  yellow,
  green
}