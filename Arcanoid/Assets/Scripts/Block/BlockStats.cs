using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStats : MonoBehaviour
{
    [SerializeField]
    private int _health = 1;
    private bool _invulnerability = false;

    public int Health { get => _health; set => _health = value; }
    public bool Invulnerability { get => _invulnerability; set => _invulnerability = value; }
}
