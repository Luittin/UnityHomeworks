using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem
{
    private static SaveSystem _saveSystem = null;

    private SaveSystem()
    {
        
    }

    public static SaveSystem Instantiate()
    {
        if (_saveSystem == null)
        {
            return _saveSystem = new SaveSystem();
        }

        return _saveSystem;
    }

    public void Save()
    {
        
    }

    public void Load()
    {
        
    }
}
