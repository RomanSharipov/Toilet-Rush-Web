using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event Action WasKeyDown;
    public event Action WasKeyUp;

    public Vector3 MousePosition => _mousePosition;

    private Vector3 _mousePosition = new Vector3();

    private void Update()
    {
        _mousePosition = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            WasKeyDown?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            WasKeyUp?.Invoke();
        }
    }
}
