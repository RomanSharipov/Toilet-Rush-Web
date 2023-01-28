using UnityEngine;

public class UnitEditor : MonoBehaviour
{
  private const string Female = "Female";
  private const string Male = "Male";

  [SerializeField] private SpriteRenderer _currentSkin;
  [SerializeField] private Sprite[] _skins;
  [SerializeField] private Animator _animator;
  [SerializeField] private RuntimeAnimatorController[] _animatorControllers;

  [Space(10)] [SerializeField] private Gender _gender;
  [SerializeField] private string _actualGender;
  [SerializeField] private Unit _unit;
  [SerializeField] private Color[] _unitColorsLines;
  [SerializeField] private Color _currentColorLine;

  private Sprite _currentUnit;

  public string ActualGender => _actualGender;

  private void OnValidate()
  {
    _animator.runtimeAnimatorController = _animatorControllers[(int) _gender];
    _currentUnit = _skins[(int) _gender];
    _actualGender = _gender.ToString();
    _currentSkin.sprite = _currentUnit;
    _currentColorLine = _unitColorsLines[(int) _gender];
    _unit.SetGender(_gender, _currentColorLine);
  }

  private void Start()
  {
    if (_actualGender == Gender.Female.ToString())
    {
      _currentSkin.sprite = _skins[SkinSaver.LoadFemaleSkin()];
      _animator.runtimeAnimatorController = _animatorControllers[SkinSaver.LoadFemaleSkin()];
    }
    else
    {
      _currentSkin.sprite = _skins[SkinSaver.LoadMaleSkin()];
      _animator.runtimeAnimatorController = _animatorControllers[SkinSaver.LoadMaleSkin()];
    }
  }
}

public enum Gender
{
  Male = 0,
  Female
}