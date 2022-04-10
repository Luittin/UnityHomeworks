using UnityEngine;
using UnityEngine.UI;

public class UpperMenuController : MonoBehaviour
{
    [SerializeField] 
    private Image[] _lifeBalls;

    public void OnUpdateLefeMenu(int countBalls)
    {
        var count = countBalls;
        for (int i = 0; i < _lifeBalls.Length; i++)
        {
            if (count > 0)
            {
                _lifeBalls[i].enabled = true;
                count--;
            }
            else
            {
                _lifeBalls[i].enabled = false;
            }
        }
    }
    
    public void OnPauseMenu()
    {
        
    }
}
