using UnityEngine;

public class PlayerInfoLeaderboard
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerInfoLeaderboard(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public PlayerInfoLeaderboard()
    {
        Name = "Anonymous";
        Score = 97;
    }

    public void Init(string name, int score)
    {
        Debug.Log($"Init name = {name} score = {score}");
        Name = name;
        Score = score;
    }
}