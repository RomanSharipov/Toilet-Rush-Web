using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardSaver 
{
    public const string LeaderBoardScore = "LeaderBoardScore";

    public static void AddScoreLeaderBoard()
    {
        Leaderboard.GetPlayerEntry(YandexLeaderboard.LeaderboardName, (result) =>
        {
            if (result == null)
            {
                Debug.Log("Player is not present in the leaderboard.");
            }

            else
            {
                Debug.Log($"result.score = {result.score}");
                int scoreLeaderBoard = result.score + 1;
                Debug.Log($"scoreLeaderBoard = {scoreLeaderBoard}");
                Leaderboard.SetScore(YandexLeaderboard.LeaderboardName, scoreLeaderBoard);
            }
        });
    }
}
