using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _stars;

    public void SetStars(int countStars)
    {
        var count = countStars - 1;
        for(int i = 0; i < _stars.Count; i++)
        {
            if(i > count)
            {
                _stars[i].SetActive(true);
            }
        }
    }


}
