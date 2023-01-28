using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1.0f;
    [SerializeField] private AudioSource _bangSound;

    private void Start()
    {
        _bangSound.Play();
        Destroy(gameObject, _lifeTime);
    }
}
