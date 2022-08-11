using System;
using UnityEngine;

[Serializable]
public class GameSetting
{
    [SerializeField]
    private float _soundVolume;
    [SerializeField]
    private float _musicVolume;

    public float SoundVolume { get => _soundVolume; set => _soundVolume = value; }
    public float MusicVolume { get => _musicVolume; set => _musicVolume = value; }
}
