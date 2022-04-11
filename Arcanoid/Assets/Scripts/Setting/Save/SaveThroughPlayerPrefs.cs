using UnityEngine;

public class SaveThroughPlayerPrefs : SaveOrLoad
{
    [SerializeField]
    private string _keyForSave;

    public override GameData Load()
    {
        string data = PlayerPrefs.GetString(_keyForSave);
        return JsonUtility.FromJson<GameData>(data);
    }

    public override void Save(GameData gameData)
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(_keyForSave, data);
    }
}
