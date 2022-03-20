using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    public AudioClip AudioClip { get => audioClip; set => audioClip = value; }
}
