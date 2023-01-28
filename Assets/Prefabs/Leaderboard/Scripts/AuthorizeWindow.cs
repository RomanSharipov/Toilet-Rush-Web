using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizeWindow : MonoBehaviour
{
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Gamestopper _gameStopper;

    public event Action AuthorizationWasSuccessful;

    private void OnButtonClick()
    {
        PlayerAccount.Authorize(AuthorizationSuccessful, OnErrorCallback);
        Close();
    }

    public void Open()
    {
        _gameStopper.Stop();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        _gameStopper.Play();
        gameObject.SetActive(false);
    }

    public void OnErrorCallback(string error)
    {
        Debug.Log($"Authorize Error : {error}");
    }

    private void AuthorizationSuccessful()
    {
        AuthorizationWasSuccessful?.Invoke();
    }

    private void OnEnable()
    {
        _authorizeButton.onClick.AddListener(OnButtonClick);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _authorizeButton.onClick.RemoveListener(OnButtonClick);
        _closeButton.onClick.RemoveListener(Close);
    }
}
