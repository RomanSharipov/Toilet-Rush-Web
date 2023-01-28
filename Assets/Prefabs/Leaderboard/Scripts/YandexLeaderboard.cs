using Agava.YandexGames;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using Leaderboard = Agava.YandexGames.Leaderboard;

public class YandexLeaderboard : MonoBehaviour
{
    public const int MaxCountPlayersOnLeaderBoard = 15;

    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private AuthorizeWindow _authorizeWindow;

    List<PlayerInfoLeaderboard> _top5PlayersArray = new List<PlayerInfoLeaderboard>();

    public const string LeaderboardName = "ToiletRushLeaderBoard";

    private const string PlayerScore = "PlayerScore";

    private bool _leaderboardGetEntriesCallFirstTime = true;

    private void Awake()
    {
        if (_authorizeWindow == null)
        {
            _authorizeWindow = FindObjectOfType<AuthorizeWindow>();
        }
    }

    private void OnEnable()
    {
        _leaderboardGetEntriesCallFirstTime = true;
    }

    public void FormListOfTopPlayers()
    {
#if UNITY_EDITOR
        AddFakeDataPlayers();
        _leaderboardView.Open();
        return;
#endif
        if (PlayerAccount.IsAuthorized == false)
        {
            _authorizeWindow.Open();
            return;
        }

        if (PlayerAccount.HasPersonalProfileDataPermission == false)
        {
            PlayerAccount.RequestPersonalProfileDataPermission(LeaderboardGetEntries, OnErrorCallback);
            _leaderboardView.Open();
            return;
        }

        //if (_leaderboardGetEntriesCallFirstTime)
        //{
        //    LeaderboardGetEntries();
        //}
        LeaderboardGetEntries();
        _leaderboardView.Open();

    }

    private void LeaderboardGetEntries()
    {
        _leaderboardGetEntriesCallFirstTime = false;

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            _top5PlayersArray.Clear();
            Debug.Log($"My rank = {result.userRank}");
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                    name = "Anonymous";
                Debug.Log(name + " " + entry.score);
                _top5PlayersArray.Add(new PlayerInfoLeaderboard(name, entry.score));
            }
            _leaderboardView.ConstructLeaderboard(_top5PlayersArray);
        });
    }

    private void AddFakeDataPlayers()
    {
        _top5PlayersArray.Clear();

        for (int i = 0; i < 9; i++)
        {
            _top5PlayersArray.Add(new PlayerInfoLeaderboard($"{i}.Руслан Зайнуллин", i));
        }

        _leaderboardView.ConstructLeaderboard(_top5PlayersArray);

        return;
    }

    public void OnErrorCallback(string error)
    {
        Debug.Log($"RequestPersonalProfileDataPermission Error : {error}");
    }


}