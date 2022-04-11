
using UnityEngine;

public class SaveOrLoad : MonoBehaviour
{
    public virtual void Save(GameData gameData)
    {

    }

    public virtual GameData Load()
    {
        return null;
    }
}
