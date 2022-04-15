using UnityEngine;

public class SaveThroughPlayerPrefs : SaveOrLoad
{
    private string _keyForSave = "GameSave";

    public GameData Load()
    {
        string data = PlayerPrefs.GetString(_keyForSave);
        return JsonUtility.FromJson<GameData>(data);
    }

    public void Save(GameData gameData)
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(_keyForSave, data);
    }
}
