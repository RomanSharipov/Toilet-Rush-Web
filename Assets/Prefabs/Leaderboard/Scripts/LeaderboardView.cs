using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private Transform _container;
    [SerializeField] private Gamestopper _gameStopper;
    [SerializeField] private List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();

    private void OnEnable()
    {
        _gameStopper.Stop();
    }

    private void OnDisable()
    {
        _gameStopper.Play();
    }

    public void ConstructLeaderboard(List<PlayerInfoLeaderboard> playersInfo)
    {
        ClearLeaderboard();

        for (int i = 0; i < YandexLeaderboard.MaxCountPlayersOnLeaderBoard && i < playersInfo.Count; i++)
        {
            LeaderboardElement newleaderboardElement = Instantiate(_leaderboardElementPrefab, _container);
            newleaderboardElement.Construct(playersInfo[i].Name, playersInfo[i].Score);
            _spawnedElements.Add(newleaderboardElement);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
            Destroy(element.gameObject);

        _spawnedElements = new List<LeaderboardElement>();

    }

    public void Close()
    {
        gameObject.SetActive(false);

    }

    public void Open()
    {
        gameObject.SetActive(true);

    }
}
