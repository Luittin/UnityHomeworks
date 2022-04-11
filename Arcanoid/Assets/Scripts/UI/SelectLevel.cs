using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : SelectedMenuItem
{
    [SerializeField]
    private List<GameObject> _stars;

    public void SetStars(int countStars)
    {
        if (countStars <= _stars.Count) 
        {
            for (int i = 0; i < countStars; i++)
            {
                _stars[i].SetActive(true);
            } 
        }
    }

    public override void OnButtonDown()
    {
        LevelSetting.Instantiate().LevelNumber = _number;
        SceneManager.LoadScene("GameScene");
    }
}
