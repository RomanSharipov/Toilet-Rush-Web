using UnityEngine;

public static class SkinSaver
{
  private const string Female = "Female";
  private const string Male = "Male";
  public const string IndexNewSkin = "IndexNewSkin";

  public static void SaveFemaleSkinIndex(int index)
  {
    ES3.Save(Female, index);
  }

  public static void SaveMaleSkinIndex(int index) => ES3.Save(Male, index);

  public static int LoadMaleSkin() => ES3.Load(Male, 0);

  public static int LoadFemaleSkin() => ES3.Load(Female, 1);
}