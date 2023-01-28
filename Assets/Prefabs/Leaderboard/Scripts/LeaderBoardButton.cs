using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private Button _leaderBoardButton;
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;

    private void OnEnable()
    {
        _leaderBoardButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _leaderBoardButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _yandexLeaderboard.FormListOfTopPlayers();

    }
}
