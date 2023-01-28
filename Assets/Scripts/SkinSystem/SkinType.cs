using UnityEngine;

public class SkinType : MonoBehaviour
{
  [SerializeField] private Gender _gender;
  [SerializeField] private int _skinIndex;

  public int Index => _skinIndex;
  public Gender Gender => _gender;
}