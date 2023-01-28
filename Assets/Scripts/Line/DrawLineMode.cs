using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrawLineMode : MonoBehaviour
{
    private float _positionLineZ = -0.05f;

    public float PositionLineZ => _positionLineZ;

    public abstract void Draw(Ray ray, out RaycastHit hit);

}

