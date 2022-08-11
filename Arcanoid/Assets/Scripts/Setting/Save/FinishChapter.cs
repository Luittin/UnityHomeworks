using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FinishChapter
{
    [SerializeField]
    private List<int> _complitedLevels;

    public List<int> ComplitedLevels 
    {
        get 
        { 
            return _complitedLevels; 
        } set
        {
            if (_complitedLevels == null)
            {
                _complitedLevels = value;
            }
        }
    }
}
