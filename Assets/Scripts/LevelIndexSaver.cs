using Agava.YandexGames;

public static class LevelIndexSaver
{
    public const string Level = "Level";

    

    public static void SaveLevel(int index)
    {
        ES3.Save(Level, index);
    }

    public static int LoadLevel() => ES3.Load(Level, 0);


}