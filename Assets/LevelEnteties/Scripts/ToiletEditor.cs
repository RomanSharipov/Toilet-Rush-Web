using UnityEngine;

public class ToiletEditor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _currentGenderLogo;
    [SerializeField] private SpriteRenderer _currentToiletType;
    [Header("Lists")]
    [SerializeField] private Sprite[] _toiletsLogos;
    [SerializeField] private Sprite[] _toiletsTypes;

    [Header("Types")]
    [Space(10)]
    [SerializeField] private ToiletGender _gender;
    [SerializeField] private ToiletType _type;
    [SerializeField] private string _actualGender;
    [SerializeField] private string _actualTypeToilet;

    public string ActualGender => _actualGender;
    public string ActualTypeToilet => _actualTypeToilet;

    private void OnValidate()
    {
        _actualTypeToilet = _type.ToString();
        _actualGender = _gender.ToString();
        _currentGenderLogo.sprite = _toiletsLogos[(int)_gender];
        _currentToiletType.sprite = _toiletsTypes[(int)_type];
    }
}

enum ToiletType
{
    Cabine = 0,
    Toilet
}

public enum ToiletGender
{
    Male = 0,
    Female,
    Universal
}