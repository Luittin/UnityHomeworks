using UnityEditor;
using UnityEngine;

public class LoaderAssets<T> where T : ScriptableObject
{
    public static T GetAsset(string path) 
    {
        Debug.Log(path);
        return AssetDatabase.LoadAssetAtPath<T>(path);
        //return Resources.Load<T>(path);
    }

    public static T[] GetAssets(string path)
    {
        return Resources.LoadAll<T>(path);
    }

    public static void CreateAsset(T createObject, string path)
    {
        AssetDatabase.CreateAsset(createObject, path);
    }

    public static void DeliteAsset(string path)
    {
        AssetDatabase.DeleteAsset(path);
    }
}
