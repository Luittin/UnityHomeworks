using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action OnJumpPress;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        OnJumpPress?.Invoke();
    }
}
