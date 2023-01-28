using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSkins")]
public class DataSkins : ScriptableObject
{
    [SerializeField] private SkinPanel[] _skinPanels;

    public int CountSkins => _skinPanels.Length;

    public SkinPanel GetSkin(int index)
    {
        return _skinPanels[index];
    }
}
