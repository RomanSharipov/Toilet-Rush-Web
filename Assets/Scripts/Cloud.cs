using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private AudioSource _fightSound;

    private void Start()
    {
        _fightSound.Play();
    }
}
